using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SubmachineGun : WeaponClass
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent<UIManager>(out uiManager);
        damage = 2;
        fireRate = 10;
        ammo = 100;
        ChangeWeaponSprite();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        fireCoolDown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCoolDown <= 0 && ammo > 0) // Change The Input To The Arcade Input
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
       * burst gun, de gun schiet 3 kogels snel achter elkaar en deze doen meer damage dan normal.
       */
    }
    public override void UpgradeTwo()
    {
        /*
         * spray & pray, de gun heeft een enorm hoge attack speed maar doet veel minder damage per bullet.
         */
    }
}
