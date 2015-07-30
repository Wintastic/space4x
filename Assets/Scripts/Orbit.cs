using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public float speed = 10f;
	public GameObject center;

	
	void Update () {
		float centerSpinSpeed = 0;
		if(center.GetComponent<Spin>() != null) {
			centerSpinSpeed = center.GetComponent<Spin>().speed;
		}
		transform.position = rotateAroundPivot(transform.position, transform.parent.position, 
		                                       Quaternion.Euler(0, (speed - centerSpinSpeed) * Time.deltaTime, 0));
	}

	private Vector3 rotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * (point - pivot) + pivot;
	}
}
