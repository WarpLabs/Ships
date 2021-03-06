﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	public float MoveForce;

	private Rigidbody2D rb2d;

	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

	}

	void FixedUpdate () {

		UpdateMove ();
		transform.rotation = PlayerMovement.PointAtMouse2D(transform.position);

	}

	void UpdateMove () {

		Vector2 moveForce = Vector2.zero;

		float horMove = Input.GetAxis ("Horizontal") * MoveForce * Time.deltaTime;
		float verMove = Input.GetAxis ("Vertical") * MoveForce * Time.deltaTime;

		moveForce = new Vector2 (horMove, verMove);

		rb2d.AddForce (moveForce, ForceMode2D.Impulse);

	}

	public static Quaternion PointAtMouse2D(Vector3 position) {

		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
		diff = new Vector3 (diff.x, diff.y);

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		return Quaternion.Euler(0f, 0f, rot_z);

	}

}
