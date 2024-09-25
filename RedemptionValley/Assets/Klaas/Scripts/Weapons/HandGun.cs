using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandGun : WeaponClass
{
    [Header("Weapon Updrages")]
    public GameObject bulletUpdrageOne;
    public GameObject bulletUpgradeTwo;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent<UIManager>(out uiManager);
      //  gameObject.TryGetComponent<UIManager>(out uiManager);
        damage = 5;
        fireRate = 5;
        ammo = 32;
        ChangeWeaponSprite();
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
                    FireBasic(bullet);
                    break;

                case CurrentUpgrade.UpdrageOne:
                    FireBasic(bullet);
                    break;

                case CurrentUpgrade.UpgradeTwo:
                    FireBasic(bullet);
                    break;

                default:
                    FireBasic(bullet);
                    break;
            }
            fireCoolDown = 5f / fireRate;
        }

    }

    public override void UpgradeOne()
    {
        currentUpgrade = CurrentUpgrade.UpdrageOne;
        damage += 35;
        fireRate = 4;
        bullet = bulletUpdrageOne;
        /*
         * big damage gun, deze upgrade path heeft een lagere fire rate maar een significant hogere damage.
         */
    }
    public override void UpgradeTwo()
    {
        currentUpgrade = CurrentUpgrade.UpgradeTwo;
        damage += 8;
        fireRate = 8;
        bulletforce = 15f;
        bullet = bulletUpgradeTwo;
        /*
         * high firerate gun, deze upgrade path heeft een stuk hogere attack speed en minder damage But the bullets bounce of the walls
         */
    }
}
