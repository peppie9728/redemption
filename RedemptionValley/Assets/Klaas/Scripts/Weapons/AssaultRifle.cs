using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponClass
{
    [Header("Upgrade One")]
    public float grenadeTimer;
    public float grenadeCountDownT;
    public GameObject grenadeBullet;

    [Header("Upgrade Two")]
    public ParticleSystem flameThrower;
    public float flameTimer;
    public float flameCountDownT;
    // Start is called before the first frame update
    void Start()
    {
        upNameOne = "Grenader";
        upNameTwo = "Flamer";
        GameObject.FindGameObjectWithTag("Player").gameObject.TryGetComponent<UIManager>(out uiManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentUpgrade == CurrentUpgrade.UpdrageOne)
        { GrenadeShooter(); }
        else if (currentUpgrade == CurrentUpgrade.UpgradeTwo)
        { 
            Flamethrower();
        }

        CheckTargets();
        if (Input.GetKeyDown(shootButton) && ammo > 0)
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
                    break;
            }
        }
    }

    public override void UpgradeOne()
    {
        currentUpgrade = CurrentUpgrade.UpdrageOne;
        //Debug.Log("Reached One...");
        /*
         * grenade launcher attachment, deze schiet om een bepaald aantal seconden een granaat in de richting van de aim van de character.
         */

       // throw new System.NotImplementedException();
    }

    public void GrenadeShooter()
    {
        grenadeCountDownT -= Time.deltaTime;

        if(grenadeCountDownT < 0)
        {
            FireBasic(grenadeBullet);
            //bullet = grenadeBullet;
            grenadeCountDownT = grenadeTimer;
        }
    }

    public override void UpgradeTwo()
    {
        currentUpgrade = CurrentUpgrade.UpgradeTwo;
        //Debug.Log("Reached Two...");
        /*
         * flamethrower attachment, deze schiet om een bepaald aantal seconden een golf van vuur die de enemy’s over time damage doet.
         */

       // throw new System.NotImplementedException();
    }

    public void Flamethrower()
    {
        flameCountDownT -= Time.deltaTime;

        if (flameCountDownT < 0)
        {
            flameThrower.Play();
            flameCountDownT = flameTimer;
        }

    }
}
