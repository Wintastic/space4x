using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3(20, 30, 20);
		gameObject.transform.localEulerAngles = new Vector3(30, 225, 0);	
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: Handle user input
	}
}
