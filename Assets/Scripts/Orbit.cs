using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public float speed = 10f;
	public GameObject center;
	float centerSpinSpeed;
	public bool drawOrbitOnHover = true;

	private GameObject orbitIndicator;
	private Vector3 prevCenterPosition;

	private Color c1 = new Color(0.24f,0.70f,1f, 0.9f);
	private Color c2 = new Color(0.24f,0.70f,1f, 0.1f);

	private LineRenderer orbitRenderer;

	private Transform myTransform;
	private Transform orbitIndicatorTransform;
	private Transform centerTransform;
	private Transform cameraTransform;

	void Start() {
		addOrbitIndicator();
		prevCenterPosition = center.transform.position;
		centerSpinSpeed = 0;
		if(center.GetComponent<Spin>() != null) {
			centerSpinSpeed = center.GetComponent<Spin>().speed;
		}
		orbitIndicatorTransform = orbitIndicator.transform;
		myTransform = transform;
		centerTransform = center.transform;
		cameraTransform = Camera.main.transform;
		orbitRenderer = orbitIndicator.GetComponent<LineRenderer>();
	}

	
	void Update () {
		myTransform.position = rotateAroundPivot(myTransform.position, centerTransform.position, 
		                                       Quaternion.Euler(0, (speed - centerSpinSpeed) * Time.deltaTime, 0));

		orbitIndicatorTransform.position += centerTransform.position - prevCenterPosition;
		prevCenterPosition = centerTransform.position;
		float w = Vector3.Distance(cameraTransform.position, centerTransform.position) / 400;
		orbitRenderer.SetWidth(w, w);
	}

	void OnMouseOver() {
		orbitRenderer.SetColors(c1, c1);
	}

	void OnMouseExit() {
		orbitRenderer.SetColors(c2, c2);
	}

	private Vector3 rotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * (point - pivot) + pivot;
	}

	private void addOrbitIndicator() {
		orbitIndicator = new GameObject();

		float radius = Vector3.Distance(transform.position, center.transform.position);
		int segments = 100 + (int)radius;

		LineRenderer r = orbitIndicator.AddComponent<LineRenderer>();
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
