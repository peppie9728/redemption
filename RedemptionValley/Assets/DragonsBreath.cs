using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsBreath : Bullet
{
    [Header("Dragons Breath Animation")]
    [SerializeField] private Animator fireAnimation;

    [Header("Dragons Breath Damage")]
    public int burnDamage = 1;
    public int burnTicks = 5;
    // Update is called once per frame
    void Update()
    {
        LifeTime();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageEnemy(collision);
        if(collision.gameObject.layer == 6)
        collision.gameObject.GetComponent<Enemy>().TimeDamage(damage,burnTicks);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageEnemy(collision);
        if (collision.gameObject.layer == 6)
            collision.gameObject.GetComponent<Enemy>().TimeDamage(damage, burnTicks);

    }
}
