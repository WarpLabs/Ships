using UnityEngine;
using System.Collections;

public class SelectorMouseControl : MonoBehaviour {

	public float GridSize;

	void LateUpdate () {

		UpdateMove ();

	}

	void UpdateMove () {

		Vector2 newSelectorPosition = GetMouseGridPos ();
		transform.position = newSelectorPosition;

	}

	Vector2 GetMouseGridPos () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float mouseX = mousePos.x;
		float mouseY = mousePos.y;

		mouseX /= GridSize;
		mouseX = Mathf.Round (mouseX);
		mouseX *= GridSize;

		mouseY /= GridSize;
		mouseY = Mathf.Round (mouseY);
		mouseY *= GridSize;

		Vector2 mouseGridPos = new Vector2 (mouseX, mouseY);

		return mouseGridPos;

	}



}
