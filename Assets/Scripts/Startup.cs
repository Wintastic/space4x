using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Material earthMaterial = Resources.Load("Materials/Earth/Earth") as Material;
		Material sunMaterial = Resources.Load("Materials/Sun") as Material;

		Star sun = new Star("Sun", new Vector3(0,0,0), 10, sunMaterial);
		ArrayList planets = new ArrayList();


		for(int i = 0; i < 10; i++) {
			// TODO Change x/z random to randomize within a radius of sun instead of square
			int x = Random.Range(-200, 200);
			int y = 0;
			int z = Random.Range(-200, 200);
			int r = Random.Range(1, 4);
			int spinSpeed = Random.Range(-20, 21);

			Planet p = new Planet("Planet" + i, new Vector3(x, y, z), r, earthMaterial, spinSpeed, sun);
			planets.Add(p);
		}	
	}
}
