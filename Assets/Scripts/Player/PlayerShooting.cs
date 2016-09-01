using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public GameObject Projectile;
	public Transform Shooter;
	public string ShootAxis;
	public float ShootCooldown;

	public float ShotSpeed;

	private bool ShootReady = true;

	private bool ShootingEnabled = false;

	private Rigidbody2D rb2d;

	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

	}

	void Update () {

		if (Input.GetAxis (ShootAxis) > 0 && ShootReady && ShootingEnabled)
			StartCoroutine (Shoot ());


	}

	IEnumerator Shoot () {

		GameObject laser = Instantiate (Projectile, Shooter.position, transform.rotation) as GameObject;
		laser.transform.RotateAround (laser.transform.position, laser.transform.forward, -90f);
		laser.transform.Translate (Vector3.forward);

		Rigidbody2D laserRb2d = laser.GetComponent<Rigidbody2D> ();
		laserRb2d.AddForce (rb2d.velocity + (new Vector2(laser.transform.up.x, laser.transform.up.y) * ShotSpeed));

		ShootReady = false;

		yield return new WaitForSeconds (ShootCooldown);

		ShootReady = true;

	}

	public void EnableShooting () {

		ShootingEnabled = true;

	}

	public void DisableShooting () {

		ShootingEnabled = false;

	}

}
