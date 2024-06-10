using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : WeaponClass
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent<UIManager>(out uiManager);
        damage = 2;
        fireRate = 10;
        ammo = 100;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        fireCoolDown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCoolDown <= 0 && ammo > 0) // Change The Input To The Arcade Input
        {
            FireBasic();
            fireCoolDown = 5f / fireRate;
        }
    }
}
