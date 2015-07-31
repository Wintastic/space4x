using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopLeftBar : MonoBehaviour {

	Text posText;

	void Start () {
		posText = GameObject.Find("PositionText").GetComponent<Text>();
	}

	void Update () {
		Vector3 pos = Camera.main.transform.position;

		posText.text = "Camera pos: X: " + pos.x + " Y: " + pos.y + " Z: " + pos.z;
	}
}
