using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject TargetObject;

	private Vector3 Offset;

	void Start () {

		Offset = transform.position - TargetObject.transform.position;

	}

	void LateUpdate () {

		transform.position = TargetObject.transform.position + Offset;

	}

}
