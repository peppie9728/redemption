using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponClass
{
  
    void Start()
    {
        damage = 5;
        fireRate = 5;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
    }
}
