using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    public Vector2 direction;
    Transform spawnPoint;
    Transform endPoint;



    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        rb.velocity = direction * moveSpeed * Time.deltaTime;
    }

    void Update()
    {
        if (target != null && direction.magnitude == 0)
        {
            direction = target.transform.position.normalized;
        }
        Move();
    }
}
