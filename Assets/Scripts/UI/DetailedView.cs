using UnityEngine;
using System.Collections;

public class DetailedView : MonoBehaviour {

	Transform myTransform;

	private GameObject camera;
	public float bodyRadius;

	void Start() {
		myTransform = transform;

		camera = GameObject.Find("Main Camera");
	}

	void OnMouseUpAsButton() {
		camera.GetComponent<CameraMovement>().enterDetailedView(myTransform, bodyRadius);
	}

}

