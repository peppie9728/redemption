using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    
    
    
    
    public override void Attack()
    {
        PlayerController player = target.GetComponent<PlayerController>();

        player.AddHealth(-damage);
    }

    public override void Move()
    {
        if (target != null)
        {
            Vector2 temp = target.transform.position - transform.position;
            temp = temp.normalized;
            rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
        }   
    }

    private void Update()
    {
        Move();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }
}
