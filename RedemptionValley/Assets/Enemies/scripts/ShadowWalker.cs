using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWalker : Enemy
{
    public float TpCdValue;
    //TpCd = teleport cooldown
    float TpCd;
    public float TpDistance;

    private void Update()
    {
        Move();
    }

    public override void Attack()
    {
        target.GetComponent<PlayerController>().AddHealth(-damage);
    }

    public override void Move()
    {
        if (TpCd > 0)
        {
            Vector2 temp = target.transform.position - transform.position;
            temp = temp.normalized;
            enemyAnimator.SetFloat("y", temp.y);
            enemyAnimator.SetFloat("x", temp.x);
           // agent.SetDestination(target.transform.position);
            //agent.
            rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
            TpCd -= Time.deltaTime;
        }
        else
        {
            Teleport();
            TpCd = TpCdValue;
        }
    }


    private void Teleport()
    {
        Vector2 dir = transform.position - target.transform.position;
        dir = dir.normalized;
        Vector2 pos2D = transform.position;
        
        transform.position = pos2D + (dir * TpDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }
}
