using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Material earthMaterial = Resources.Load("Materials/Earth/Earth") as Material;
		Material sunMaterial = Resources.Load("Materials/Sun") as Material;
		Material moonMaterial = Resources.Load("Materials/Moon/Moon") as Material;

		Star sun = new Star("Sun", new Vector3(0,0,0), 10, sunMaterial);
		IList planets = new ArrayList();


		for(int i = 0; i < 10; i++) {
			// TODO Change x/z random to randomize within a radius of sun instead of square
			int x = Random.Range(-200, 200);
			int y = 0;
			int z = Random.Range(-200, 200);
			float r = Random.Range(1, 4);
			float spinSpeed = Random.Range(-20, 21);

			Planet p = new Planet("Planet" + i, new Vector3(x, y, z), r, earthMaterial, spinSpeed, sun);
			planets.Add(p);

			int nSatellites = Random.Range(0, 4);
			for(int j = 0; j < nSatellites; j++) {
				float d = Random.Range(2*r, 8*r);
				Vector3 satellitePosition = new Vector3(x+d, y, z);
				float satelliteRadius = (float)Random.Range(r, r*5) / 10;
				float satelliteSpinSpeed = Random.Range(-20, 21);
				p.addSatellite("Satellite"+j+p.name,satellitePosition, satelliteRadius, moonMaterial, satelliteSpinSpeed);
			}
		}

		startUI();
	}

	void startUI() {
		GameObject.Find("SimpleHoverInfoCanvas").GetComponent<Canvas>().enabled = false;
	}
}
