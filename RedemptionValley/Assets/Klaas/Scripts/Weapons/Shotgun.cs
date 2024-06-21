using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponClass
{


    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent<UIManager>(out uiManager);
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
                    FireSpread();
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
}
