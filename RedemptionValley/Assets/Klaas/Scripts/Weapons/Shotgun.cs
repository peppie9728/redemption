using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : WeaponClass
{
    [Header("Upgrade Bullets")]
    public GameObject upgradeOneBullet;
    public GameObject upgradeTwoBullet;

    // Start is called before the first frame update
    void Start()
    {
      GameObject.FindGameObjectWithTag("Player").gameObject.TryGetComponent<UIManager>(out uiManager);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        timer -= Time.deltaTime;
        if ( timer <= 0 && ammo > 0) // Change The Input To The Arcade Input
        {
            switch (currentUpgrade)
            {
                case CurrentUpgrade.Basic:
                    if (Input.GetButtonDown("Fire1"))
                    {
                        FireBasic();
                        timer = fireCoolDown / fireRate;
                    }
                    break;

                case CurrentUpgrade.UpdrageOne:
                    if (Input.GetButton("Fire1"))
                    {
                        FireSpread();
                        timer = fireCoolDown / fireRate;
                    }
                    break;

                case CurrentUpgrade.UpgradeTwo:
                    if (Input.GetButtonDown("Fire1"))
                    {
                        FireSpread();
                        timer = fireCoolDown / fireRate;
                    }
                    break;

                default:
                    FireBasic();
                    break;
            }
           
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            UpgradeTwo();
        }
    }
    public override void UpgradeOne()
    {
        currentUpgrade = CurrentUpgrade.UpdrageOne;
        fireCoolDown = 1;
        fireRate = 20f;
        damage = 2;
        bulletLifeTime = 0.18f;
        /*
         * automatic shotgun, deze upgrade zorgt dat de shotgun volledig automatisch word maar levert daar wel een beetje damage voor in en voegt birdshot toe en kleinere range.
         */
    }
    public override void UpgradeTwo()
    {
        currentUpgrade = CurrentUpgrade.UpgradeTwo;
        bullet = upgradeTwoBullet;
        fireCoolDown = 3;
        fireRate = 2;
        damage = 5;
        bulletLifeTime = 1f;
        /*
         * dragon breath, met deze upgrade schiet je shotgun vuur ammo die een knock back geeft en een damage over time effect aan de enemy’s geeft.
         */
    }

}
