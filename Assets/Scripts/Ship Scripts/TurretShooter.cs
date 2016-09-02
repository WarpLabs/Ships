using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {


	public GameObject Bullet;
	public Transform bulletSpawn;
	private GameObject bullet;
	public LayerMask enemy;


	public float bulletSpeed;
	public float fireWait;
	public float damage;
	public float knockBack;
	public float stun;
	public float range;
	public float lifespan;

	private Collider2D target;
	private Vector2 direction;


    void Start()
    {
        StartCoroutine(Shoot());
    }

	IEnumerator Shoot()
	{
        while (true)
        {
            if (target != null)
            {
                bullet = (GameObject)Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation);
                bullet.transform.parent = transform;
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);

                ProjectileExplosion explo = bullet.GetComponent<ProjectileExplosion>();
                explo.Damage = damage;
                explo.Knockback = knockBack;
                explo.Stun = stun;
                explo.Invoke("Explode", lifespan);

                yield return new WaitForSeconds(fireWait);
            }
            else
            {
                yield return null;
            }
        }
	}


    void Update()
    {
        target = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), range, enemy);

        if (target != null)
        {
            direction = (target.gameObject.transform.position - transform.position).normalized;

            Vector3 dir = target.gameObject.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

	//IEnumerator Start()
	//{
	//    while (true)
	//    {
	//        target = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), range, enemy);


	//        if (target != null)
	//        {
	//            direction = (target.gameObject.transform.position - transform.position).normalized;


	//            Vector3 dir = target.gameObject.transform.position - transform.position;
	//            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
	//            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



	//        }
	//        else
	//        {
	//            Debug.Log("nothin");

	//        }
	//    }
	//}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.parent == transform)
		{
			Destroy(other.gameObject);
		}
	}

}
