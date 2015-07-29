using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float mouseSensitivity = 1.0f;
	public float zoomSpeed = 50.0f;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3(0, 40, -60);
		gameObject.transform.localEulerAngles = new Vector3(30, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(0)){
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
			lastPosition = Input.mousePosition;
			float x = Mathf.Clamp(transform.position.x, -200, 200);
			float y = Mathf.Clamp(transform.position.y, 0, 100);
			float z = Mathf.Clamp(transform.position.z, -200, 0);
			transform.position = new Vector3(x, y, z);
		}

		float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheelDelta != 0f) {
			transform.Translate(Vector3.forward*zoomSpeed*scrollWheelDelta);
		}
	}
}
