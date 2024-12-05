using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

    [Header("Input Info")]
    [SerializeField] protected KeyCode shootButton;

    [Header("UI Info")]
    public string weaponName;
    public Sprite weaponSprite;
   
    [Header("Weapon")]
    public int damage;
    public float fireRate = 5;
    public float fireCoolDown = 1f;
    public float timer;
    [Header("Ammo")]
    [Range(0,9999)]public int ammo;
    //public int minAmmo;
    public int maxAmmo;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletforce;
    public Transform firePoint;
    public GameObject fireSprite;
    public float bulletLifeTime;
    [Header("Target")]
    [SerializeField]private Transform dontAsk;
    public Transform fireTarget;
    public Collider2D[] hitColliders;
    public LayerMask layerMask;
    public UIManager uiManager;

    [Header("Current Weapon State")]
    public CurrentUpgrade currentUpgrade;
    public Bullet test1;

    [Header("Weapon Upgrade")]
    public string upgradeOneInfo;
    public string upgradeTwoInfo;
    public string upNameOne;
    public string upNameTwo;

    public abstract void UpgradeOne();
    public abstract void UpgradeTwo();

    public void FireBasic(GameObject bullet)
    {
        if (fireTarget != null)
        {
            ammo -= 1;
            GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            test1 = firedBullet.GetComponent<Bullet>();
            test1.damage = damage;
            firedBullet.GetComponent<Bullet>().bulletLife = bulletLifeTime;
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
    public async void BurstFire(uint burstAmount, int bulletDelayMS)
    {
        if(fireTarget != null )
        {
            
            for (int i = 0; i < burstAmount; i++) 
            {
                if (ammo > 0)
                {
                    ammo -= 1;
                    GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

                    Vector2 direction = (fireTarget.position - firePoint.position).normalized;
                    rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
                    firedBullet.GetComponent<Bullet>().damage = damage;
                    firedBullet.GetComponent<Bullet>().bulletLife = bulletLifeTime;
                    StartCoroutine(fireExplosion());
                    uiManager.UpdateAmmo();
                    await Task.Delay(bulletDelayMS);
                }
                else { break; }
            }
        }
    }
    public void FireSpread() // On fire it shoots 3 bullets that spread away from each other
    {
        if (fireTarget != null)
        {
            Vector3 spreadOffset = new Vector3(0, -0.2f, 0);
            ammo -= 1;
            for (int i = 0; i < 3; i++)
            {

                GameObject firedBullet = Instantiate(bullet, firePoint.position + spreadOffset, firePoint.rotation);
                Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
                firedBullet.GetComponent<Bullet>().damage = damage;
                firedBullet.GetComponent<Bullet>().bulletLife = bulletLifeTime;
                Vector2 direction = (fireTarget.position - firePoint.position).normalized;
                rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
                spreadOffset.y += 0.2f;
                StartCoroutine(fireExplosion());
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
