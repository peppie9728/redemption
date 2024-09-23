using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("Targeting")]
    public float bulletLife = 1f;
    public Transform target;

    [Header("Damage")]
    public int damage;
    //public float speed = 8f;
    //public float rotateSpeed = 99999f;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void LifeTime()
    {
        bulletLife -= Time.deltaTime / 2;
        if (bulletLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void DamageEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }
    }
    public void DamageEnemy(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }

    }
    private void FixedUpdate()
    {
        //Vector2 direction = (Vector2)target.position - rb.position;

        //direction.Normalize();

        //float rotateAmount = Vector3.Cross(direction, transform.up).z;

        //rb.angularVelocity = -rotateAmount * rotateSpeed;

        //rb.velocity = transform.up * speed;
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
   
     
        }
        Destroy(this.gameObject);
        /*
         *Add Code To Damage Enemy
         
    }*/
}
