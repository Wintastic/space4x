﻿using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public float speed = 10f;
	public GameObject center;
	public bool drawOrbitOnHover = true;

	private GameObject orbit;
	private Vector3 prevCenterPosition;

	private Color c1 = new Color(0.24f,0.70f,1f, 0.9f);
	private Color c2 = new Color(0.24f,0.70f,1f, 0.1f);

	void Start() {
		addOrbitIndicator();
		prevCenterPosition = center.transform.position;
	}

	
	void Update () {
		float centerSpinSpeed = 0;
		if(center.GetComponent<Spin>() != null) {
			centerSpinSpeed = center.GetComponent<Spin>().speed;
		}
		transform.position = rotateAroundPivot(transform.position, transform.parent.position, 
		                                       Quaternion.Euler(0, (speed - centerSpinSpeed) * Time.deltaTime, 0));

		orbit.transform.position += center.transform.position - prevCenterPosition;
		prevCenterPosition = center.transform.position;
		float w = Vector3.Distance(Camera.main.transform.position, center.transform.position) / 400;
		orbit.GetComponent<LineRenderer>().SetWidth(w, w);
	}

	void OnMouseOver() {
		orbit.GetComponent<LineRenderer>().SetColors(c1, c1);
	}

	void OnMouseExit() {
		orbit.GetComponent<LineRenderer>().SetColors(c2, c2);
	}

	private Vector3 rotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * (point - pivot) + pivot;
	}

	private void addOrbitIndicator() {
		orbit = new GameObject();

		float radius = Vector3.Distance(transform.position, center.transform.position);
		int segments = 100 + (int)radius;

		LineRenderer r = orbit.AddComponent<LineRenderer>();
		r.useWorldSpace = false;
		r.material = new Material(Shader.Find("Particles/Additive"));
		r.SetColors(c2, c2);
		r.SetWidth(0.2f, 0.2f);
		r.SetVertexCount(segments+1);
		
		float angle = 0f;
		
		for(int i = 0; i < segments+1; i++) {
			float x = radius*Mathf.Sin(Mathf.Deg2Rad * angle);
			float z = radius*Mathf.Cos(Mathf.Deg2Rad * angle);
			
			Vector3 pos = new Vector3(center.transform.position.x + x, center.transform.position.y, center.transform.position.z + z);
			r.SetPosition(i, pos);
			angle += 360f / segments;
		}
	}
}
