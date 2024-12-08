using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour
{
    public static event HandleEnemyDeath OnEnemyDeath;
    public delegate void HandleEnemyDeath(int points);

    public float health;
    public float moveSpeed;
    public float damage;
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected Rigidbody2D rb;

    [Header("Enemy Animator")]
    public Animator enemyAnimator;
    [Header("Item")]
    public GameObject item;

    [Header("Nav Mesh")]
   [SerializeField] protected NavMeshAgent agent;

     public void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed; 

        rb = GetComponent<Rigidbody2D>();
        FindTarget();
        //ScaleEnemy();
    }

    private void FixedUpdate()
    {
        FindTarget();
        if (health <= 0)
        {
            OnEnemyDeath?.Invoke(10);
            DropItem(30);
            Destroy(gameObject);
        }
    }

    public abstract void Attack();

    public abstract void Move();


    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float input)
    {
        health = input;
    }
    public void AddHealth(float input)
    {
        health += input;
    }

    IEnumerator TakeTimeDamage(int damage, int damageTicks)
    {
        for (int i = 0; i < damageTicks; i++)
        {
            health -= damage;
            yield return new WaitForSeconds(2);
        }
    }
    public void TimeDamage(int damage, int damageTicks)
    {
        StartCoroutine(TakeTimeDamage(damage, damageTicks));
    }

    public void Destroy()
    {
        GameObject.Destroy(this);
    }

    public void FindTarget()
    {
        GameObject[] targetsTemp = GameObject.FindGameObjectsWithTag("Player");
        if (targetsTemp.Length > 1)
        {
            if (Vector2.Distance(targetsTemp[0].transform.position,transform.position) < Vector2.Distance(targetsTemp[1].transform.position, transform.position))
            {
                target = targetsTemp[0];
            }
            else
            {
                target = targetsTemp[1];
            }
        }
        else
        {
            target = targetsTemp[0];
        }
    }

    public void ScaleEnemy()
    {
        float damageToAdd = damage / 9 * 4;
        damage += Mathf.Round(damageToAdd);

        float healthToAdd = health / 8 * 4;
        health += Mathf.Round(healthToAdd);

        Debug.Log($"Current EnemyHealth:{health} - Current Enemy Damage:{damage}");

    }
    public void DropItem(int goldAmount)
    {
        int randomNum = Random.Range(0, 10);
        if (randomNum < 4)
        {
            GameObject droppedItem = Instantiate(item, gameObject.transform.position, Quaternion.identity);
            if (randomNum <= 2)
            {
                droppedItem.GetComponent<PickUps>().itemType = ItemType.Gold;
                droppedItem.GetComponent<PickUps>().itemAmount = goldAmount;
            } 
            else
            {
                droppedItem.GetComponent<PickUps>().itemType = ItemType.Ammo;
                droppedItem.GetComponent<PickUps>().itemAmount = goldAmount;
            }

            //Drop Item
        }
         
    }
}
