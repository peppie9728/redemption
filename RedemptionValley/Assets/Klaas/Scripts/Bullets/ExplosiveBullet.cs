using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [Header("Explosion")]
    [SerializeField] private GameObject explosionObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        DamageEnemy(collision);

        if (collision.gameObject.layer == 6)
        {
            Collider2D[] explosionRadius = Physics2D.OverlapCircleAll(collision.transform.position, 5f, 6);


            foreach (var enemy in explosionRadius)
            {
                enemy.GetComponent<Enemy>().health -= damage;
            }

            GameObject explosion = Instantiate(explosionObject, gameObject.transform.position, Quaternion.identity);

            await Task.Delay(90);
            Destroy(explosion);
            await Task.Delay(10);
            //Destroy(this.gameObject);
        }
    }
}
