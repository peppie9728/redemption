using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy
{
    [SerializeField]
    GameObject spellPrefab;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    float distanceToKeep;
    [SerializeField]
    float spellForce;
    [SerializeField]
    float spellCDValue;
    float spellCD;

    private void Start()
    {
        FindTarget();
    }
    public override void Attack()
    {
        if (spellCD <= 0)
        {
            GameObject firedBullet = Instantiate(spellPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
            firedBullet.GetComponent<Spell>().damage = damage;
            Vector2 direction = (target.transform.position - firePoint.position).normalized;
            rb.AddForce(direction * spellForce, ForceMode2D.Impulse);
            spellCD = spellCDValue;
        }
        else
        {
            spellCD -= Time.deltaTime;
        }
    }
    public override void Move()
    {
        if (Vector2.Distance(target.transform.position, transform.position) > distanceToKeep)
        {
            Vector2 temp = target.transform.position - transform.position;
            temp = temp.normalized;
            enemyAnimator.SetFloat("y", temp.y);
            enemyAnimator.SetFloat("x", temp.x);
             rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
           //agent.SetDestination(target.transform.position - transform.position);
        }
        else
        {
            Vector2 temp = target.transform.position + transform.position;
            temp = temp.normalized;
           // agent.SetDestination(target.transform.position + transform.position);
           rb.velocity = new Vector2(temp.x * moveSpeed * Time.fixedDeltaTime, temp.y * moveSpeed * Time.fixedDeltaTime);
        }
        
    }



    private void Update()
    {

        Move();
        Attack();
    }
}
