using UnityEngine;
using System.Collections;

public class HealthControl : MonoBehaviour {
    
    public float health;
    private float totalHealth;
    private float percentHealth;
    public Transform HealthBar;

    public float weight;
    private float bulletKnockback;
    private float knockBackDone;
    private Vector2 knockBackDirection;

    public float armor;
    private float bulletDamage;
    private float damageDone;
    

    void Start()
    {
        totalHealth = health;
    }

	void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.parent.gameObject.CompareTag("Gun"))
        {
            bulletDamage = other.transform.parent.gameObject.GetComponent<GunShooter>().damage;
            damageDone = bulletDamage - armor;
            health -= damageDone;
            percentHealth = health / totalHealth;

            bulletKnockback = other.transform.parent.gameObject.GetComponent<GunShooter>().knockBack;
            knockBackDone = (bulletKnockback - weight) / 10f;
            knockBackDirection = (other.transform.position - other.transform.parent.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(knockBackDone * knockBackDirection);

            if (damageDone > 0)
            {       
                HealthBar.localScale = new Vector3(percentHealth, HealthBar.localScale.y, HealthBar.localScale.z);
                HealthBar.position = new Vector3(HealthBar.position.x - ((damageDone/totalHealth) / 2), HealthBar.position.y, HealthBar.position.z);
            }
                        
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
