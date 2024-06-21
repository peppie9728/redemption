using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1.5f;
    public Transform target;
    
    //public float speed = 8f;
    //public float rotateSpeed = 99999f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletLife -= Time.deltaTime /2;
        if(bulletLife <= 0)
        {
            Destroy(gameObject);
        }
       // transform.LookAt(target);

    }
    private void FixedUpdate()
    {
        //Vector2 direction = (Vector2)target.position - rb.position;

        //direction.Normalize();

        //float rotateAmount = Vector3.Cross(direction, transform.up).z;

        //rb.angularVelocity = -rotateAmount * rotateSpeed;

        //rb.velocity = transform.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6)
        {
            Destroy(collision.gameObject);
           // Destroy(gameObject);
        }
        Destroy(this.gameObject);
        /*
         *Add Code To Damage Enemy
         */
    }
}
