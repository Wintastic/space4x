using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleHoverInfo : MonoBehaviour {
	private GameObject simpleText;
	private GameObject canvas;
	private Canvas hoverCanvas;
	private Transform canvasTransform;
	private Text bodyName;
	private Text position;
	private bool enabled = false;
	private Transform infoPanelTransform;

	private Transform gameObjectTransform;
	private Camera mainCamera;

	public float bodyRadius;

	void Start () {
		canvas = GameObject.Find("SimpleHoverInfoCanvas");
		bodyName = canvas.transform.FindChild ("InfoPanel").transform.FindChild ("BodyNameText").GetComponent<Text>();
		position = canvas.transform.FindChild ("InfoPanel").transform.FindChild ("PositionText").GetComponent<Text>();
		hoverCanvas = canvas.GetComponent<Canvas>();
		infoPanelTransform = canvas.transform.FindChild("InfoPanel").transform;

		gameObjectTransform = gameObject.transform;
		mainCamera = Camera.main;
	}

	void OnMouseOver() {
		enabled = true;
		hoverCanvas.enabled = true;
		bodyName.text = gameObject.name;
		position.text = "Radius"+bodyRadius;
	}

	void OnMouseExit() {
		enabled = false;
		hoverCanvas.enabled = false;
	}

	void Update() {
		if (enabled) {
			infoPanelTransform.position = (mainCamera.WorldToScreenPoint(gameObjectTransform.position + new Vector3(bodyRadius, bodyRadius, 0f))) + new Vector3(-30f, -120f, 0f);
		}
	}
}
