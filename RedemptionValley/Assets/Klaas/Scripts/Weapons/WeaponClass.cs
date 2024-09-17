using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum CurrentUpgrade
{ 
    Basic,UpdrageOne,UpgradeTwo
}
public abstract class WeaponClass : MonoBehaviour
{
    public static event HandleEnemyTransform OnClosestEnemy;
    public delegate void HandleEnemyTransform(Transform enemyPosition);

    [Header("UI Info")]
    public string weaponName;
    public Sprite weaponSprite;
    [Header("Weapon")]
    public int damage;
    public float fireRate = 5;
    public float fireCoolDown = 1f;
    [Header("Ammo")]
    public uint ammo;
    //public int minAmmo;
    public uint maxAmmo;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletforce;
    public Transform firePoint;
    public GameObject fireSprite;

    [Header("Target")]
    [SerializeField]private Transform dontAsk;
    public Transform fireTarget;
    public Collider2D[] hitColliders;
    public LayerMask layerMask;
    public UIManager uiManager;

    [Header("Current Weapon State")]
    public CurrentUpgrade currentUpgrade;
    public Bullet test1;

    public abstract void UpgradeOne();
    public abstract void UpgradeTwo();

    public void FireBasic()
    {
        Debug.Log("Fire");
        if (fireTarget != null)
        {
            ammo -= 1;
            GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            test1 = firedBullet.GetComponent<Bullet>();
            test1.damage = damage;
            Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (fireTarget.position - firePoint.position).normalized;
            rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);

            uiManager.UpdateAmmo();
            StartCoroutine(fireExplosion());
        }
    }
    IEnumerator fireExplosion()
    {
        fireSprite.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fireSprite.SetActive(false);
    }
    public void CheckTargets()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, 10f, layerMask);

        if (hitColliders.Length > 0)
        {

            fireTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D hitCollider in hitColliders)
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    fireTarget = hitCollider.transform;
                    OnClosestEnemy?.Invoke(hitCollider.transform);
                }
            }
        }
        else { fireTarget = dontAsk; OnClosestEnemy?.Invoke(null); }
      
    }
    public void FireSpread()
    {
        if (fireTarget != null)
        {
            Vector3 spreadOffset = new Vector3(0, -0.2f, 0);
            ammo -= 1;
            for (int i = 0; i < 3; i++)
            {

                GameObject firedBullet = Instantiate(bullet, firePoint.position + spreadOffset, firePoint.rotation);
                Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

                Vector2 direction = (fireTarget.position - firePoint.position).normalized;
                rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
                spreadOffset.y += 0.2f;
            }
             uiManager.UpdateAmmo();
        }
    }
    public void ChangeWeaponSprite()
    {
       gameObject.GetComponent<SpriteRenderer>().sprite = weaponSprite;

    }
    // Update is called once per frame
    void Update()
    {


    }

}