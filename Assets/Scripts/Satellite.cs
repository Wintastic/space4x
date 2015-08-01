using UnityEngine;
using System.Collections;

public class Satellite : Body {
	private float radius;
	public Sphere sphere;
	
	public Satellite(string name, Vector3 position, float radius, Material material, float spinSpeed, Planet planet) : base(name) {
		this.radius = radius;
		this.sphere = new Sphere(name, position, radius, Constants.satelliteLongitudeSegments, Constants.satelliteLatitudeSegments, material);
		this.sphere.gameObject.AddComponent<Spin>().speed = spinSpeed;
		Orbit orbit = this.sphere.gameObject.AddComponent<Orbit>();
		orbit.center = planet.sphere.gameObject;
		float distance = Vector3.Distance(sphere.gameObject.transform.position, planet.sphere.gameObject.transform.position);
		orbit.speed = Constants.satelliteOrbitConstant / distance;

		sphere.gameObject.transform.SetParent(planet.sphere.gameObject.transform);

		SimpleHoverInfo simpleHoverInfo = this.sphere.gameObject.AddComponent<SimpleHoverInfo>();
		simpleHoverInfo.bodyRadius = radius;
	}
}
