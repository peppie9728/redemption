using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    public Vector2 direction;
    Transform spawnPoint;
    float endPoint;


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
        if (target != null && direction.magnitude == 0)
        {
            direction = target.transform.position - transform.position;
            direction = direction.normalized;
        }
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Attack();
        }
    }
}
