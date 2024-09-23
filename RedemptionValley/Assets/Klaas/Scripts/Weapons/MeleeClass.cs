using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeClass : MonoBehaviour
{
    [Header("Basic Melee")]
    public GameObject hitCollider;
    public float attackSpeed;
    public int meleeDamage;

    [Header("Upgrade State")]
    public CurrentUpgrade currentMeleeUpgrade;

    [Header("Upgrade One")]

    [Header("Upgrade Two")]


    [Header("Melee Time")]
    public float attackDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch(currentMeleeUpgrade)
        {
            case CurrentUpgrade.Basic:
                 MeleeAttack();
                break;
            case CurrentUpgrade.UpdrageOne:
                MeleeUpgradeOne();
                break;
            case CurrentUpgrade.UpgradeTwo:
                MeleeUpgradeTwo();
                    break;
                default: MeleeAttack();
                break;
        }
    }
    public void MeleeAttack()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0)
        {
            StartCoroutine(AttackDelay());
            //attackDelay = 3;
        }
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
        attackDelay = 3f;
    }

    public void MeleeUpgradeOne()
    {
            attackDelay -= Time.deltaTime;
        for (int i = 0; i < 2; i++)
        {
            if (attackDelay < 0)
            {
                StartCoroutine(AttackDelay());
                //attackDelay = 3;
            }
        }
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
