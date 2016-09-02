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

	private Rigidbody2D rb2d;

    public float armor;
    private float bulletDamage;
    private float damageDone;
    

    void Start()
    {
        totalHealth = health;
		if (GetComponent<Rigidbody2D>() != null)
			rb2d = GetComponent<Rigidbody2D> ();
    }

	void OnTriggerEnter2D(Collider2D other)
    {

		if (LayerMask.LayerToName(other.gameObject.layer) == "PlayerProjectiles")
        {
			ProjectileExplosion explo = other.GetComponent<ProjectileExplosion> ();

			bulletDamage = explo.Damage;
            damageDone = bulletDamage - armor;
            health -= damageDone;
            percentHealth = health / totalHealth;

			if (rb2d != null) {

				bulletKnockback = explo.Knockback;
				knockBackDirection = other.transform.up;
				rb2d.AddForce (knockBackDirection * bulletKnockback);

			}

            if (damageDone > 0)
            {       
                HealthBar.localScale = new Vector3(percentHealth, HealthBar.localScale.y, HealthBar.localScale.z);
                HealthBar.position = new Vector3(HealthBar.position.x - ((damageDone/totalHealth) / 2), HealthBar.position.y, HealthBar.position.z);
            }                        
            Destroy(other.gameObject);
        }

        if (other.transform.parent.gameObject.CompareTag("Turret"))
        {
            bulletDamage = other.transform.parent.gameObject.GetComponent<TurretShooter>().damage;
            damageDone = bulletDamage - armor;
            health -= damageDone;
            percentHealth = health / totalHealth;

            bulletKnockback = other.transform.parent.gameObject.GetComponent<TurretShooter>().knockBack;
            knockBackDone = (bulletKnockback - weight) / 10f;
            knockBackDirection = (other.transform.position - other.transform.parent.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(knockBackDone * knockBackDirection);

            if (damageDone > 0)
            {
                HealthBar.localScale = new Vector3(percentHealth, HealthBar.localScale.y, HealthBar.localScale.z);
                HealthBar.position = new Vector3(HealthBar.position.x - ((damageDone / totalHealth) / 2), HealthBar.position.y, HealthBar.position.z);
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
