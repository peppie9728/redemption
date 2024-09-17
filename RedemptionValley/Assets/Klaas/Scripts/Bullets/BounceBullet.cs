using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BounceBullet : Bullet
{
    Vector2 direction;
    public float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(1, 1).normalized;
    }
  
    // Update is called once per frame
    void Update()
    {
        LifeTime();
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
        }

        //  Vector2 inDirection = rb.velocity;
        // Vector2 inNormal = collision.contacts[0].normal;
        // Vector2 newVolocity = Vector2.Reflect(inDirection, inNormal);
        Vector2 surfaceNormal = collision.contacts[0].normal;

        // Calculate the reflected direction vector
        direction = Vector2.Reflect(direction, surfaceNormal);

    }
}
