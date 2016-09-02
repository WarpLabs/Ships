using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {


    public GameObject Bullet;
    public Transform bulletSpawn;
    private GameObject bullet;


    public float bulletSpeed;
    public float fireWait;
    public float damage;
    public float knockBack;
    public float stun;


    IEnumerator Start()
    {
        while (true)
        {
            

            if (Input.GetKey(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.Space) == true)
            {
                bullet = (GameObject)Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation);
                bullet.transform.parent = transform;
                bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.right * bulletSpeed);
                yield return new WaitForSeconds(fireWait);
            }

            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == transform)
        {
            Destroy(other.gameObject);
        }
    }

}
