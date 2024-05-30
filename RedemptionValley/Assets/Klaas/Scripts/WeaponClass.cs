using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
    [Header("Weapon")]
    public string weaponName;
    public int damage;
    public float fireRate = 5;
    public float fireCoolDown = 1f;
    [Header("Bullet")]
    public GameObject bullet;
    public float bulletforce;
    public Transform firePoint;

    public Transform fireTarget;
    public Collider2D[] hitColliders;
    public LayerMask layerMask;
    public void Fire()
    {
        Debug.Log("Fire");
        if (fireTarget != null)
        {
            GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (fireTarget.position - firePoint.position).normalized;
            rb.AddForce(direction * bulletforce, ForceMode2D.Impulse);
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
        fireCoolDown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireCoolDown <= 0) // Change The Input To The Arcade Input
        {
            Fire();
            fireCoolDown = 5f / fireRate;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

}
