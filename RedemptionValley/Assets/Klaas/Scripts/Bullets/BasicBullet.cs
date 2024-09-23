using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{

    public void Update()
    {
        LifeTime();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageEnemy(collision);

    }
}
