using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	private Transform myTransform;
	public float speed = 10f;

	void Start() {
		myTransform = transform;
	}

	void Update () {
		myTransform.Rotate(Vector3.up, speed * Time.deltaTime);		
	}
}
