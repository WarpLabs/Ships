using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {

	public float MoveSpeed;

	void FixedUpdate () {

		transform.position += transform.up * MoveSpeed * Time.deltaTime;

	}

}
