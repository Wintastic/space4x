using UnityEngine;
using System.Collections;

public class Star : Body {
	private float radius;
	public Sphere sphere;
	
	public Star(string name, Vector3 position, float radius, Material material) : base(name) {
		this.radius = radius;
		this.sphere = new Sphere(name, position, radius, Constants.starLongitudeSegments, Constants.starLatitudeSegments, material);

		GameObject light = new GameObject(name + "Light");
		light.transform.position = sphere.gameObject.transform.position;
		Light l = light.AddComponent<Light>();
		l.type = LightType.Point;
		//TODO: set range and intensity based on size and strength
		l.range = 1000;
		l.intensity = 1;

		SimpleHoverInfo simpleHoverInfo = this.sphere.gameObject.AddComponent<SimpleHoverInfo>();
		simpleHoverInfo.bodyRadius = radius;

		DetailedView detailedView = this.sphere.gameObject.AddComponent<DetailedView>();
		detailedView.bodyRadius = radius;
	}
}
