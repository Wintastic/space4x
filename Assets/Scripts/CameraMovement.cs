using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float mouseSensitivity = 1.0f;
	public float panSpeed = 1.0f;
	public float zoomSpeed = 50.0f;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3(0, 40, -60);
		gameObject.transform.localEulerAngles = new Vector3(30, 0, 0);
	}

	// Update is called once per frame
	void Update () {

		panCamera ();
		zoomCamera();
		clampCamera();
	}

	void panCamera() {
		float z = transform.position.z;

		//Mouse panning
		if (Input.GetMouseButtonDown(2)) {
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(2)){
			Vector3 delta = lastPosition - Input.mousePosition;
			transform.Translate(delta.x * mouseSensitivity * panSpeed, delta.y * mouseSensitivity * panSpeed, 0);
			lastPosition = Input.mousePosition;
		}

		//Arrow key panning
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.Translate(0, 1 * panSpeed, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(0, -1 * panSpeed, 0);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(-1 * panSpeed, 0, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(1 * panSpeed, 0, 0);
		}

		//Prevent changes in Z position
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	void zoomCamera(){
		float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheelDelta != 0f) {
			transform.Translate(Vector3.forward*zoomSpeed*scrollWheelDelta);
		}
	}

	void clampCamera(){
		float x = Mathf.Clamp(transform.position.x, -200, 200);
		float y = Mathf.Clamp(transform.position.y, 0, 100);
		float z = Mathf.Clamp(transform.position.z, -200, 200);
		transform.position = new Vector3(x, y, z);
	}
}
