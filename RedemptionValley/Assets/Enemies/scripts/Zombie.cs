using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    
    
    
    
    public override void Attack()
    {
        throw new System.NotImplementedException();
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

}
