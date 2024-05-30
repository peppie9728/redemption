using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    Rigidbody2D rb;
    
    
    
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        rb.velocity();
    }
}
