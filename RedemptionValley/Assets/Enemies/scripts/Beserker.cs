using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beserker : Enemy
{
    public float chargeCdValue;
    float chargeCd;

    public float chargeUpTimeValue;
    public float chargeSpeed;

    float chargeUpTime;
    bool hasCharged;
    bool isCharging;
    Vector2 TempTarget;
    Vector2 startPos;
    private void Awake()
    {
        base.Awake();
        chargeCd = chargeCdValue;
        chargeUpTime = chargeUpTimeValue;
    }

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
        if (chargeCd > 0)
        {
            Vector2 temp = target.transform.position - transform.position;
            temp = temp.normalized;
            enemyAnimator.SetFloat("y", temp.y);
            enemyAnimator.SetFloat("x", temp.x);
            rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
            chargeCd -= Time.deltaTime;
        }
        else
        {
            if (!hasCharged)
            {
                Charge();
            }
            else
            {
                chargeCd = chargeCdValue;
                hasCharged = false;
            }
        }
    }


    public void Charge()
    {
        if (chargeUpTime <= 0)
        {
            Vector2 temp = transform.position;
            if (Vector2.Distance(temp,startPos) < 10)
            {
                rb.velocity = new Vector2(TempTarget.x * chargeSpeed * Time.fixedDeltaTime, TempTarget.y * chargeSpeed * Time.fixedDeltaTime);
            }
            else
            {
                hasCharged = true;
                chargeUpTime = chargeUpTimeValue;
            }
        }
        else
        {
            rb.velocity = new Vector2(0,0);
            chargeUpTime -= Time.deltaTime;
            TempTarget = target.transform.position - transform.position;
            TempTarget.Normalize();
            Vector2 temp = transform.position;
            startPos = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }
}
