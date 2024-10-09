using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponClass
{
    [Header("Upgrade One")]
    public GameObject explosiveBullet;
    [Header("Upgrade Two")]
    public GameObject armorPiercing;
    // Start is called before the first frame update
    void Start()
    {
        upNameOne = "Explosive";
        upNameTwo = "Armor";
        GameObject.FindGameObjectWithTag("Player").TryGetComponent<UIManager>(out uiManager);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        fireCoolDown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireCoolDown <= 0 && ammo > 0) // Change The Input To The Arcade Input
        {
            switch (currentUpgrade)
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpgradeOne();
        }
    }
    public override void UpgradeOne()
    {
        currentUpgrade = CurrentUpgrade.UpdrageOne;
        bullet = explosiveBullet;
        damage = 10;
        /*
         * explosive ammo, deze upgrade zorgt ervoor dat de sniper explosieve ammo schiet die aoe damage doet.
         */
    }
    public override void UpgradeTwo()
    {
        currentUpgrade = CurrentUpgrade.UpgradeTwo;
        bullet = armorPiercing;
        fireCoolDown = 8;
        fireRate = 1;
        damage = 30;
        /*
         * armor piercing ammo, deze upgrade zorgt ervoor dat de sniper door meerdere enemy’s tegelijk kan schieten.
         */
    }
}
