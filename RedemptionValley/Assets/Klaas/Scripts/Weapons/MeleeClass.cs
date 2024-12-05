using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class MeleeClass : MonoBehaviour
{
    [Header("Animator")]
    public Animator meleeAnimations;

    [Header("Basic Melee")]
    public GameObject hitCollider;
    public float attackSpeed;
    public float meleeDamage;
    public float meleeRange;

    public EdgeCollider2D upgCollider;

    [Header("Upgrade State")]
    public CurrentUpgrade currentMeleeUpgrade;

    [Header("Upgrade One")]
    public int loopAmount;
    public int currentLoopAmount;
    [Header("Upgrade Two")]


    [Header("Melee Time")]
    public float attackDelay = 3f;
    public int millisecondDelay;
    // Start is called before the first frame update
    void Start()
    {
      meleeAnimations = GetComponent<Animator>();   
        
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

           // default: MeleeAttack();
                //break;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            MeleeUpgradeOne();
        }
    }
    public async void MeleeAttack()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0)
        {
            await Task.Delay(millisecondDelay);
            //  StartCoroutine(AttackDelay());
            //attackDelay = 3;
            attackDelay = 3f;
        }
    }
    public void SetMeleeOn()
    {
        hitCollider.SetActive(true);
    }
    public void SetMeleeOff()
    {
        hitCollider.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
           // Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().health -= meleeDamage;
        }
    }
    public void UpgradeAttackAmount()
    {
        
    }

    public void UpgradeMeleeDamage()
    {
        meleeDamage *= 1.5f;
    }
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.layer == 6)
    //    {
    //        collision.gameObject.GetComponent<Enemy>().health -= meleeDamage;
    //    }
    //}
    IEnumerator AttackDelay()
    {
      // meleeAnimations.

        hitCollider.SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        hitCollider.SetActive(false);
        attackSpeed = 0.5f;
       
    }

    public void MeleeUpgradeOne()
    {
        upgCollider.isTrigger = false;
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0)
        {
            attackDelay = 3;

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
