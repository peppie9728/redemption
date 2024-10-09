using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : Enemy
{
    public Animator enemyAnimator;

    public override void Attack()
    {
        target.GetComponent<PlayerController>().AddHealth(-damage);
    }

    public override void Move()
    {
        if (target != null)
        {
            Vector2 temp = target.transform.position - transform.position;
            temp = temp.normalized;
            rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
            enemyAnimator.SetFloat("y", temp.y);
            enemyAnimator.SetFloat("x", temp.x);
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
