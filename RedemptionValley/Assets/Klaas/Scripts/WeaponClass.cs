using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    [Header("Weapon")]
    public string weaponName;
    public int damage;
    public float fireRate = 5;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletforce;
    public Transform firePoint;

   public Transform fireTarget;
    public Collider2D[] hitColliders;
    public LayerMask layerMask;
    public void Fire()
    {
        GameObject firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        firedBullet.GetComponent<Bullet>().target = fireTarget;

        Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse); // firepoint moet locatie van een enemy zijn
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        hitColliders = Physics2D.OverlapCircleAll(transform.position, 10f, layerMask);
       
        if(hitColliders.Length > 0)
        {
            fireTarget = hitColliders[0].transform;
        }
        if(Input.GetButtonDown("Fire1")) // Change The Input To The Arcade Input
        {
            Fire();
        }
    }


}
