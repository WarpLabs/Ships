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

    private Collider2D target;
    private Vector2 direction;


    IEnumerator Shoot()
    {
        bullet = (GameObject)Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        yield return new WaitForSeconds(2f);
    }


    void Update()
    {
        target = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), range, enemy);


        if (target != null)
        {
            direction = (target.gameObject.transform.position - transform.position).normalized;


            Vector3 dir = target.gameObject.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            StartCoroutine(Shoot());
        }
        else
        {
            Debug.Log("nothin");
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
