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
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }

    }
}
