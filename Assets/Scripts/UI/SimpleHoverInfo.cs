using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleHoverInfo : MonoBehaviour {
	private GameObject simpleText;
	private GameObject canvas;
	private Text bodyName;
	private Text position;
	private bool enabled = false;

	void Start () {
		canvas = GameObject.Find("SimpleHoverInfoCanvas");
		bodyName = canvas.transform.FindChild ("InfoPanel").transform.FindChild ("BodyNameText").GetComponent<Text>();
		position = canvas.transform.FindChild ("InfoPanel").transform.FindChild ("PositionText").GetComponent<Text>();
	}

	void OnMouseOver() {
		enabled = true;
		canvas.GetComponent<Canvas>().enabled = true;


		bodyName.text = gameObject.name;
		position.text = "Position";
	}

	void OnMouseExit() {
		enabled = false;
		canvas.GetComponent<Canvas>().enabled = false;
	}

	void Update(){
		if (enabled) {
			canvas.transform.FindChild("InfoPanel").transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		}
	}
}
