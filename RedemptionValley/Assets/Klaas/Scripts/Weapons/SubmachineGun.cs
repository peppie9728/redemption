using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SubmachineGun : WeaponClass
{
    [Header("Burst Options")]
    public uint burstAmount = 3;
    public int bulletDelayMS = 90;
    // Start is called before the first frame update
    void Start()
    {
        upNameOne = "Burst";
        upNameTwo = "Spray And Pray";
        GameObject.FindGameObjectWithTag("Player").gameObject.TryGetComponent<UIManager>(out uiManager);
        damage = 2;
        fireRate = 10;
        ammo = 10;
        ChangeWeaponSprite();
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
                    BurstFire(burstAmount,bulletDelayMS);
                    break;

                case CurrentUpgrade.UpgradeTwo:
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
        damage += 3;
        fireRate = 25;
        // Add A change to the sprite
      /* 
       * burst gun, de gun schiet 3 kogels snel achter elkaar en deze doen meer damage dan normal. burst ammount kan worden geupgrade
       */
    }
    public override void UpgradeTwo()
    {
        /*
         * spray & pray, de gun heeft een enorm hoge attack speed maar doet veel minder damage per bullet.
         */
    }
}
