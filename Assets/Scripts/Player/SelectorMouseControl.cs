using UnityEngine;
using System.Collections;

public class SelectorMouseControl : MonoBehaviour {

	public float GridSize;

	public GameObject RelativeObject;
	public bool RelativeGridPosition;

	public float Depth;

	[HideInInspector] public float RotationDelta;

	void LateUpdate () {

		UpdateMove ();

	}

	void UpdateMove () {

		Vector2 newSelectorPosition = GetMouseGridPos ();
		if (RelativeGridPosition) {
			transform.rotation = RelativeObject.transform.rotation;
			transform.RotateAround (transform.position, transform.forward, RotationDelta);
		}
		transform.position = newSelectorPosition;
		transform.Translate (Vector3.forward * Depth);

	}

	Vector2 GetMouseGridPos () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (RelativeGridPosition) {

			mousePos = RelativeObject.transform.InverseTransformPoint (mousePos);

		}

		float mouseX = mousePos.x;
		float mouseY = mousePos.y;

		mouseX /= GridSize;
		mouseX = Mathf.Round (mouseX);
		mouseX *= GridSize;

		mouseY /= GridSize;
		mouseY = Mathf.Round (mouseY);
		mouseY *= GridSize;

		Vector2 mouseGridPos = new Vector2 (mouseX, mouseY);

		if (RelativeGridPosition) {

			mouseGridPos = RelativeObject.transform.TransformPoint (mouseGridPos);

		}

		return mouseGridPos;

	}

}
