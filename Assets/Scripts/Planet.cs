using UnityEngine;
using System.Collections;

public class Planet : Body {
	private int radius;
	private Sphere sphere;

	public Planet(string name, Vector3 position, int radius, Material material, float spinSpeed, Star star) : base(name) {
		this.radius = radius;
		this.sphere = new Sphere(name, position, radius, Constants.planetLongitudeSegments, Constants.planetLatitudeSegments, material);
		this.sphere.gameObject.AddComponent<Spin>().speed = spinSpeed;
		Orbit orbit = this.sphere.gameObject.AddComponent<Orbit>();
		orbit.point = star.getPosition();
		float distance = Vector3.Distance(getPosition(), star.getPosition());
		orbit.speed = Constants.planetOrbitConstant / distance;
	}

	public override Vector3 getPosition() {
		return this.sphere.gameObject.transform.position;
	}
}
