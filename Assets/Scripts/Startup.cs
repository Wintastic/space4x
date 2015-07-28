using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Material m = Resources.Load("Materials/Earth") as Material;
		Sphere.createSphere(new Vector3(0, 1, 0), 1, 240, 160, m);
	
	}
}
