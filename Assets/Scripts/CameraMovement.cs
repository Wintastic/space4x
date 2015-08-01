using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Vector3 initialPosition = new Vector3(0, 80, -120);

	public float mouseSensitivity = 1.0f;
	public float panSpeed = 1.0f;
	public float zoomSpeed = 50.0f;
	private Vector3 lastPosition;

	private GameObject focusedObject = null;
	private float focusedObjectRadius = 0;
	private Vector3 prevFocusedObjectPosition = Vector3.zero;
	private bool inDetailedView = false;
	private bool exitingDetailedView = false;

	private Vector3 velocity = Vector3.zero;
	private Vector3 prevPosition;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = initialPosition;
		gameObject.transform.localEulerAngles = new Vector3(30, 0, 0);
		prevPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if(!exitingDetailedView) {
			panCamera ();
			zoomCamera();
			if(!inDetailedView) {
				clampCamera();
			}
		}

		updateDetailedView();
		prevPosition = transform.position;
	}

	void panCamera() {
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
		transform.position = new Vector3(transform.position.x, transform.position.y, prevPosition.z);
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

	public void enterDetailedView(GameObject body, float bodyRadius) {
		focusedObject = body;
		focusedObjectRadius = bodyRadius;
		prevFocusedObjectPosition = focusedObject.transform.position;
		inDetailedView = true;
	}

	private void updateDetailedView() {
		if(transform.position != prevPosition) {
			inDetailedView = false;
			exitingDetailedView = false;
		}

		if(inDetailedView) {
			Vector3 target = focusedObject.transform.position + new Vector3(0,focusedObjectRadius*2,focusedObjectRadius*-4);
			transform.position = moveTo(transform.position, target, ref velocity, 0.5f, true);
		} else if(exitingDetailedView) {
			transform.position = moveTo(transform.position, initialPosition, ref velocity, 0.5f, false);
			if(Vector3.Distance(transform.position, initialPosition) < 0.1f) {
				exitingDetailedView = false;
				focusedObject = null;
			}
		}
		if(Input.GetKeyDown("escape")) {
			inDetailedView = false;
			focusedObject = null;
			exitingDetailedView = true;			
		}
		if(focusedObject != null) {
			prevFocusedObjectPosition = focusedObject.transform.position;
		}

	}

	private Vector3 moveTo(Vector3 point, Vector3 destination, ref Vector3 velocity, float speed, bool follow) {
		Vector3 newPoint = Vector3.SmoothDamp(point, destination, ref velocity, 0.5f);
		float d = Vector3.Distance(newPoint, destination);
		if(follow && d < 5) {
			newPoint = Vector3.SmoothDamp(newPoint, destination, ref velocity, 0.1f);
		}
		print (d);
		return newPoint;
	}
}
