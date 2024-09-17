using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeClass : MonoBehaviour
{
    [Header("Basic Melee")]
    public GameObject hitCollider;
    public float attackSpeed;
    public int meleeDamage;

    [Header("")]
    public float attackDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        attackDelay -= Time.deltaTime;
        if(attackDelay <0)
        {
            StartCoroutine(AttackDelay());
            attackDelay = 3;

        } 
    }
    public void MeleeAttack()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(collision.gameObject);
            //gameObject.GetComponent<Enemy>().health -= meleeDamage;
        }
    }

    IEnumerator AttackDelay()
    {
        hitCollider.SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        hitCollider.SetActive(false);
        attackSpeed = 0.5f;
    }

    public void MeleeUpgradeOne()
    {
        /*
         * katana, de eerste upgrade voor de katana is een dubbele aanval waar hij 2 keer slaat en de 2e upgrade schiet er een wervelwind uit het zwaard.
         */
    }
    public void MeleeUpgradeTwo()
    {
        /*
         * axe, de eerste upgrade heeft een knockback en de 2e upgrade slaat de player in een cirkel om zich heen.
         */
    }
}
