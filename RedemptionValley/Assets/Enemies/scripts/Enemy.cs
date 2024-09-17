using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float damage;
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FindTarget();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public abstract void Attack();

    public abstract void Move();


    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float input)
    {
        health = input;
    }
    public void AddHealth(float input)
    {
        health += input;
    }

    public void Destroy()
    {
        GameObject.Destroy(this);
    }

    public void FindTarget()
    {
        GameObject[] targetsTemp = GameObject.FindGameObjectsWithTag("Player");
        if (targetsTemp.Length > 1)
        {
            if (Vector2.Distance(targetsTemp[0].transform.position,transform.position) < Vector2.Distance(targetsTemp[1].transform.position, transform.position))
            {
                target = targetsTemp[0];
            }
            else
            {
                target = targetsTemp[1];
            }
        }
        else
        {
            target = targetsTemp[0];
        }
    }
}
