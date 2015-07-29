using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Material earthMaterial = Resources.Load("Materials/Earth") as Material;
		Material sunMaterial = Resources.Load("Materials/Sun") as Material;
		GameObject sun = Sphere.createSphere(new Vector3(0, 0, 0), 10, 240, 160, sunMaterial);
		GameObject sunLight = new GameObject();
		Light l = sunLight.AddComponent<Light>();
		l.type = LightType.Point;
		l.range = 1000;
		l.intensity = 5;

		for(int i = 0; i < 10; i++) {
			// TODO Change x/z random to randomize within a radius of sun instead of square
			int x = Random.Range(-200, 200);
			int y = 0;
			int z = Random.Range(-200, 200);
			int r = Random.Range(1, 4);

			GameObject s = Sphere.createSphere(new Vector3(x, y, z), r, 240, 160, earthMaterial);

			Spin spinScript = s.AddComponent<Spin>();
			spinScript.speed = Random.Range(-20, 20);

			Orbit orbitScript = s.AddComponent<Orbit>();
			float distance = Vector3.Distance(s.transform.position, sun.transform.position);
			orbitScript.speed = 100f / distance;
			orbitScript.point = sun.transform.position;
		}	
	}
}
