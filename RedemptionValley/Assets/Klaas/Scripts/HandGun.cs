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
}
