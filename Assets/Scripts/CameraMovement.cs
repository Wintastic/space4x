using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Transform myTransform;

	private Vector3 initialPosition = new Vector3(0, 80, -120);

	public float mouseSensitivity = 1.0f;
	public float zoomSpeed = 50.0f;
	private Vector3 lastPosition;

	private Transform focusedObjectTransform = null;
	private float focusedObjectRadius = 0;
	private Vector3 prevFocusedObjectPosition = Vector3.zero;
	private bool inDetailedView = false;
	private bool exitingDetailedView = false;

	private Vector3 velocity = Vector3.zero;
	private Vector3 prevPosition;

	// Use this for initialization
	void Start () {
		myTransform = transform;

		myTransform.position = initialPosition;
		myTransform.localEulerAngles = new Vector3(30, 0, 0);
		prevPosition = myTransform.position;
	}

	// Update is called once per frame
	void Update () {
		panCamera ();
		zoomCamera();
		if(!inDetailedView) {
			clampCamera();
		}

		updateDetailedView();
		prevPosition = myTransform.position;
	}

	void panCamera() {
		float panSpeed = 0.01f + myTransform.position.y / 200f;
		float keyboardPanSpeed = panSpeed * 2;

		//Mouse panning
		if (Input.GetMouseButtonDown(2)) {
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(2)){
			Vector3 delta = lastPosition - Input.mousePosition;
			myTransform.Translate(delta.x * mouseSensitivity * panSpeed, delta.y * mouseSensitivity * panSpeed, 0);
			lastPosition = Input.mousePosition;
		}

		//Arrow key panning
		if (Input.GetKey(KeyCode.UpArrow)) {
			myTransform.Translate(0, 1 * keyboardPanSpeed, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			myTransform.Translate(0, -1 * keyboardPanSpeed, 0);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			myTransform.Translate(-1 * keyboardPanSpeed, 0, 0);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			myTransform.Translate(1 * keyboardPanSpeed, 0, 0);
		}

		//Prevent changes in Z position
		myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, prevPosition.z);
	}

	void zoomCamera(){
		float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheelDelta != 0f) {
			myTransform.Translate(Vector3.forward*zoomSpeed*scrollWheelDelta);
		}
	}

	void clampCamera(){
		float x = Mathf.Clamp(myTransform.position.x, -200, 200);
		float y = Mathf.Clamp(myTransform.position.y, 0, 100);
		float z = Mathf.Clamp(myTransform.position.z, -200, 200);
		myTransform.position = new Vector3(x, y, z);
	}

	public void enterDetailedView(Transform bodyTransform, float bodyRadius) {
		focusedObjectTransform = bodyTransform;
		focusedObjectRadius = bodyRadius;
		prevFocusedObjectPosition = focusedObjectTransform.position;
		inDetailedView = true;
	}

	public void exitDetailedView() {
		inDetailedView = false;
		focusedObjectTransform = null;
		prevFocusedObjectPosition = Vector3.zero;
		exitingDetailedView = true;
	}

	private void updateDetailedView() {
		if((inDetailedView || exitingDetailedView) && myTransform.position != prevPosition) {
			exitDetailedView();
			exitingDetailedView = false;
		}

		if(inDetailedView) {
			Vector3 target = focusedObjectTransform.position + new Vector3(0,focusedObjectRadius*2,focusedObjectRadius*-4);
			myTransform.position = moveTo(myTransform.position, target, ref velocity, 0.5f, true);
		} else if(exitingDetailedView) {
			myTransform.position = moveTo(myTransform.position, initialPosition, ref velocity, 0.5f, false);
			if(Vector3.Distance(myTransform.position, initialPosition) < 0.1f) {
				exitingDetailedView = false;
			}
		}
		if(Input.GetKeyDown("escape")) {
			exitDetailedView();
		}
		if(focusedObjectTransform != null) {
			prevFocusedObjectPosition = focusedObjectTransform.position;
		}

	}

	private Vector3 moveTo(Vector3 point, Vector3 destination, ref Vector3 velocity, float speed, bool follow) {
		Vector3 newPoint = Vector3.SmoothDamp(point, destination, ref velocity, 0.5f);
		float d = Vector3.Distance(newPoint, destination);
		if(follow && d < 5) {
			newPoint = Vector3.SmoothDamp(newPoint, destination, ref velocity, 0.1f);
		}
		return newPoint;
	}
}
