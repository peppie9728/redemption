using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponClass
{
 
    void Start()
    {
        gameObject.TryGetComponent<UIManager>(out uiManager);
        damage = 5;
        fireRate = 5;
        ammo = 32;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        fireCoolDown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireCoolDown <= 0 && ammo > 0) // Change The Input To The Arcade Input
        {
            switch(currentUpgrade)
            {
                case CurrentUpgrade.Basic:
                    FireBasic();
                    break;

                case CurrentUpgrade.UpdrageOne:
                    break;

                case CurrentUpgrade.UpgradeTwo:
                    break;

                default: break;
            }
            fireCoolDown = 5f / fireRate;
        }
    }

    public void UpgradeOne()
    {
        currentUpgrade = CurrentUpgrade.UpdrageOne;
        /*
         * big damage gun, deze upgrade path heeft een lagere fire rate maar een significant hogere damage.
         */
    }
    public void UpgradeTwo()
    {
        currentUpgrade = CurrentUpgrade.UpgradeTwo; 
        /*
         * high firerate gun, deze upgrade path heeft een stuk hogere attack speed en minder damage
         */
    }
}
