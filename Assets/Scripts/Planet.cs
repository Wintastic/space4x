using UnityEngine;
using System.Collections;

public class Planet : Body {
	private float radius;
	public Sphere sphere;
	private IList satellites;

	public Planet(string name, Vector3 position, float radius, Material material, float spinSpeed, Star star) : base(name) {
		this.radius = radius;
		this.sphere = new Sphere(name, position, radius, Constants.planetLongitudeSegments, Constants.planetLatitudeSegments, material);
		this.sphere.gameObject.AddComponent<Spin>().speed = spinSpeed;
		Orbit orbit = this.sphere.gameObject.AddComponent<Orbit>();
		orbit.center = star.sphere.gameObject;
		float distance = Vector3.Distance(sphere.gameObject.transform.position, star.sphere.gameObject.transform.position);
		orbit.speed = Constants.planetOrbitConstant / distance;
		satellites = new ArrayList();

		sphere.gameObject.transform.SetParent(star.sphere.gameObject.transform);

		SimpleHoverInfo simpleHoverInfo = this.sphere.gameObject.AddComponent<SimpleHoverInfo>();
	}

	public void addSatellite(string name, Vector3 position, float radius, Material material, float spinSpeed) {
		Satellite s = new Satellite(name, position, radius, material, spinSpeed, this);
		this.satellites.Add(s);
	}
}
