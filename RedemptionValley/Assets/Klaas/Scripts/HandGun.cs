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
    }

    public void UpgradeOne()
    {
        /*
         * big damage gun, deze upgrade path heeft een lagere fire rate maar een significant hogere damage.
         */
    }
    public void UpgradeTwo()
    {
        /*
         * high firerate gun, deze upgrade path heeft een stuk hogere attack speed en minder damage
         */
    }
}
