using UnityEngine;
using System.Collections;

public class HealthControl : MonoBehaviour {
    
    public float health;
    private float totalHealth;
    private float percentHealth;
    public Transform HealthBar;

    private float bulletKnockback;
    private float knockBackDone;
    private Vector2 knockBackDirection;

	private Rigidbody2D rb2d;

    public float armor;
    private float bulletDamage;
    private float damageDone;

	public bool AlwaysUp;
    

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
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

		if (AlwaysUp) {

			float parentRot = transform.localEulerAngles.z;

			HealthBar.parent.transform.localRotation = Quaternion.Euler (0, 0, -parentRot);

		}

    }


}
