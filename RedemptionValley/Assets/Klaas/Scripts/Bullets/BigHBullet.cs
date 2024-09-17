using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHBullet : Bullet
{
    [Header("Hit Count")]
    public uint maxHitCount = 3;
    public uint currentHitCount = 0;

    public void Update()
    {
        LifeTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            currentHitCount++;
            if(currentHitCount >= maxHitCount)            
            {
                //Add Damage to enemy
                Destroy(this.gameObject); 
            }
            // Add Damage to enemy
        }
    }
}
