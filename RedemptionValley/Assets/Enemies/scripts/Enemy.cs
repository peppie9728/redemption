using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public float damage;
    GameObject target;

    private void Awake()
    {
        FindTarget();
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
        float value;
        foreach (GameObject i in targetsTemp)
        {
            float distanceBetween = Vector2.Distance(i.transform.position, this.transform.position);
            if (distanceBetween != 0)
            {

            }
        }

    }
}
