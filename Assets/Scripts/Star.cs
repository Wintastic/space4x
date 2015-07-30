using UnityEngine;
using System.Collections;

public class Star : Body {
	private int radius;
	private Sphere sphere;
	
	public Star(string name, Vector3 position, int radius, Material material) : base(name) {
		this.radius = radius;
		this.sphere = new Sphere(name, position, radius, Constants.planetLongitudeSegments, Constants.planetLatitudeSegments, material);

		GameObject light = new GameObject(name + "Light");
		light.transform.position = getPosition();
		Light l = light.AddComponent<Light>();
		l.type = LightType.Point;
		//TODO: set range and intensity based on size and strength
		l.range = 1000;
		l.intensity = 1;
	}

	public override Vector3 getPosition() {
		return this.sphere.gameObject.transform.position;
	}	
}
