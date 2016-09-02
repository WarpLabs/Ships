using UnityEngine;
using System.Collections;

public class ProjectileExplosion : MonoBehaviour {

	[HideInInspector] public float Damage;
	[HideInInspector] public float Knockback;
	[HideInInspector] public float Stun;

	void OnTriggerEnter2D (Collider2D other) {

		Explode ();

	}


	void Explode () {

		Destroy (gameObject);

	}

}
