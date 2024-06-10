using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CurrentUpgrade
{ 
    Basic,UpdrageOne,UpgradeTwo
}
public abstract class WeaponClass : MonoBehaviour
{
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
    //public int mamAmmo;
    [Header("Bullet")]
    public GameObject bullet;
    public float bulletforce;
    public Transform firePoint;
    [Header("Target")]
    [SerializeField]private Transform dontAsk;
    public Transform fireTarget;
    public Collider2D[] hitColliders;
    public LayerMask layerMask;
    public UIManager uiManager;
    [Header("Current Weapon State")]
    public CurrentUpgrade currentUpgrade;
    public void FireBasic()
    {
        Debug.Log("Fire");
        if (fireTarget != null)
        {
            ammo -= 1;
            GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (fireTarget.position - firePoint.position).normalized;
            rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
            uiManager.UpdateAmmo();
        }
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
                }
            }
        }
        else { fireTarget = dontAsk; }
      
    }

    public void FireSpread()
    {
        if (fireTarget != null)
        {
            ammo -= 1;
            GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (fireTarget.position - firePoint.position).normalized;
            rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
            uiManager.UpdateAmmo();
        }
    }
    // Update is called once per frame
    void Update()
    {


    }

}
