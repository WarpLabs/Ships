using UnityEngine;
using System.Collections;

public class ProjectileExplosion : MonoBehaviour {

	public float Lifespan = 1f;

	void Start () {

		Invoke ("Explode", Lifespan);


	}

	void OnTriggerEnter2D (Collider2D other) {

		Explode ();

	}


	void Explode () {

		Destroy (gameObject);

	}

}
