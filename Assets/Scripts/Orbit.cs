using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public float speed = 10f;
	public Vector3 point = Vector3.zero;
	
	void Update () {
		//TODO: change to calculation based on difference in y of the two objects
		Vector3 axis = Vector3.up;
		transform.RotateAround(point, axis, speed * Time.deltaTime);		
	}
}
