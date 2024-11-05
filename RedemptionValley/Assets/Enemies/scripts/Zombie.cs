using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : Enemy
{
    //public Animator enemyAnimator;
    public bool isTouchingPlayer = false;
    public float attackTimer;
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
        enemyAnimator.SetBool("isTouchingPlayer", isTouchingPlayer);
        attackTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
            Attack();
            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && attackTimer < 0 )
        {
            Attack();
            
            attackTimer = 5;
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isTouchingPlayer= false;
        }
    }
}
