using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    public Vector2 direction;
    Transform spawnPoint;
    float endPoint;

    private void Start()
    {
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
    }
    public override void Attack()
    {
        target.GetComponent<PlayerController>().AddHealth(-damage);
    }

    public override void Move()
    {
        rb.velocity = new Vector2(direction.x * moveSpeed * Time.fixedDeltaTime, direction.y * moveSpeed * Time.fixedDeltaTime);
        
    }

    void Update()                                                
    {
        //if (target != null && !isTouchingWall )//&& direction.magnitude <= 0)
        //{
        //    direction = target.transform.position - transform.position;
        //    direction = direction.normalized;
          
        //} 
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attack();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
        //if (collision.gameObject.layer == 9)
        //{
        //    isTouchingWall = true;
        //}
        // Vector2 surfaceNormal = collision.contacts[0].normal;
        // direction = Vector2.Reflect(direction.normalized, surfaceNormal);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 9)
        //{
        //    isTouchingWall = false;
        //}
    }
}
