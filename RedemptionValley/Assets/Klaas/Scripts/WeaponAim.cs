using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    private Transform enemyTransform = null;
    private void OnEnable()
    {
        WeaponClass.OnClosestEnemy += SetTransform; 
    }
    private void OnDisable()
    {
        WeaponClass.OnClosestEnemy -= SetTransform; 
    }
    public void SetTransform(Transform transform)
    {
          enemyTransform = transform;
    }
    void Update()
    {
        try
        {
            transform.right = enemyTransform.position - transform.position;
        }
        catch { }
    }
}
