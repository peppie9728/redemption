using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : WeaponClass
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                    FireBasic();
                    break;

                case CurrentUpgrade.UpdrageOne:
                    break;

                case CurrentUpgrade.UpgradeTwo:
                    break;

                default:
                    FireBasic();
                    break;
            }
            fireCoolDown = 5f / fireRate;
        }
    }
    public override void UpgradeOne()
    {
        /*
         * explosive ammo, deze upgrade zorgt ervoor dat de sniper explosieve ammo schiet die aoe damage doet.
         */
    }
    public override void UpgradeTwo()
    {
        /*
         * armor piercing ammo, deze upgrade zorgt ervoor dat de sniper door meerdere enemy’s tegelijk kan schieten.
         */
    }
}
