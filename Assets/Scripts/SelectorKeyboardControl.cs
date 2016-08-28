using UnityEngine;
using System.Collections;

public class SelectorKeyboardControl : MonoBehaviour {

	public string Left;
	public string Right;
	public string Up;
	public string Down;

	public float GridSize;

	public float Debug_HoldMoveSpeed;

	private float HoldMoveSpeed = 0.1f;
	private float HoldMoveTimer = 0f;
	private bool HoldMoveAvailable = false;

	void Start () {

		HoldMoveSpeed = Debug_HoldMoveSpeed;
		HoldMoveTimer = HoldMoveSpeed;

	}

	void Update () {

		UpdateMove ();
		UpdatePlace ();


	}

	void UpdatePlace () {



	}

	void UpdateMove () {

		HoldMoveTimer -= Time.deltaTime;
		if (HoldMoveTimer < 0f) {
			HoldMoveAvailable = true;
		}

		if (HoldMoveAvailable) {

			bool check = true;

			if (Input.GetKey (Left)) {
				transform.Translate (Vector2.left * GridSize);
			} else if (Input.GetKey (Right)) {
				transform.Translate (Vector2.right * GridSize);
			} else if (Input.GetKey (Up)) {
				transform.Translate (Vector2.up * GridSize);
			} else if (Input.GetKey (Down)) {
				transform.Translate (Vector2.down * GridSize);
			} else {
				check = false;
			}

			if (check) {
				HoldMoveTimer = HoldMoveSpeed;
				HoldMoveAvailable = false;
			}

		}

	}
		
}
