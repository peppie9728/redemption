using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{


    [Header("Player Ammo")]
    public TextMeshProUGUI ammoText;

    [Header("Player Script")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponClass currentWeapon;

    private void OnEnable()
    {
        WeaponStore.OnAmmoAdd += UpdateAmmo;
    }

    private void Start()
    {
        currentWeapon = GameObject.FindGameObjectWithTag("WeaponSprite").gameObject.GetComponentInChildren<WeaponClass>();
        ammoText.text = $"{currentWeapon.ammo}";
    }


    public void UpdateAmmo()
    {
        ammoText.text = $"{currentWeapon.ammo}";
    }
    public void GetWeaponClass()
    {
        try
        {
            currentWeapon = GameObject.FindGameObjectWithTag("WeaponSprite").gameObject.GetComponentInChildren<WeaponClass>();
            ammoText.text = $"{currentWeapon.ammo}";
        }
        catch
        {
            Debug.Log("No Weapon Could Be Found");
        }
    }
}
