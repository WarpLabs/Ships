using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public GameObject Projectile;
	public Transform Shooter;
	public string ShootAxis;
	public float ShootCooldown;

	public float ShotSpeed;

	public float Damage;
	public float Knockback;
	public float Stun;

	public float Lifespan;

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

		ProjectileExplosion explo = laser.GetComponent<ProjectileExplosion> ();
		explo.Damage = Damage;
		explo.Knockback = Knockback;
		explo.Stun = Stun;
		explo.Invoke ("Explode", Lifespan);

		Rigidbody2D laserRb2d = laser.GetComponent<Rigidbody2D> ();
		laserRb2d.velocity = rb2d.velocity;
		laserRb2d.AddForce (new Vector2(laser.transform.up.x, laser.transform.up.y) * ShotSpeed);

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
