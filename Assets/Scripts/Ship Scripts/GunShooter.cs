using UnityEngine;
using System.Collections;

public class GunShooter : MonoBehaviour {

    public GameObject Bullet;
    public Transform bulletSpawn;

	private Rigidbody2D shipRb2d;

    public float bulletSpeed;
    public float fireWait;
    public float damage;
    public float knockBack;
    public float stun;
	public float lifespan;

	void Start () {

		shipRb2d = transform.parent.GetComponentInParent<Rigidbody2D> ();

		StartCoroutine (ShootingCheck ());

	}

    IEnumerator ShootingCheck()
    {

        while(true)
        {
            if (Input.GetKey(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.Space) == true)
            {
				GameObject bullet = Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

				Rigidbody2D bulletRb2d = bullet.GetComponent<Rigidbody2D> ();
				bulletRb2d.velocity = shipRb2d.velocity;
				bulletRb2d.AddForce(bulletSpawn.up * bulletSpeed);

				ProjectileExplosion explo = bullet.GetComponent<ProjectileExplosion> ();
				explo.Damage = damage;
				explo.Knockback = knockBack;
				explo.Stun = stun;
				explo.Invoke ("Explode", lifespan);

                yield return new WaitForSeconds(fireWait);  
				continue;
            }
            
            yield return null;

        }
    }

}
