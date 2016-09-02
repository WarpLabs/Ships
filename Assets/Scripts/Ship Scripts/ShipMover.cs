using UnityEngine;
using System.Collections;

public class ShipMover : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 movement;
    public float thrust;
    public float turning;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

     void FixedUpdate()
    {


        rb.AddForce(transform.up * Input.GetAxis("Vertical") * thrust * Time.deltaTime);
        rb.AddTorque(-Input.GetAxis("Horizontal") * turning * Time.deltaTime);
       
    }





}
