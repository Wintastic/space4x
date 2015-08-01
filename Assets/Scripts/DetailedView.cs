using UnityEngine;
using System.Collections;

public class DetailedView : MonoBehaviour {

	private GameObject camera;
	public float bodyRadius;

	void Start() {
		camera = GameObject.Find("Main Camera");
	}

	void OnMouseUpAsButton() {
		camera.GetComponent<CameraMovement>().enterDetailedView(gameObject, bodyRadius);
	}

}

