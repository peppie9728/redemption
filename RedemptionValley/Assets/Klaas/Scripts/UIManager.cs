using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{

 

    [Header("Player Money & Skill Points")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI skillText;

    [Header("Player Ammo")]
    public TextMeshProUGUI ammoText;

    [Header("Player Script")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponClass currentWeapon;

    private void OnEnable()
    {
        WeaponStore.OnAmmoAdd += UpdateAmmo;
        WeaponStore.OnMoneyChange += UpdateMoney;
    }

    private void Start()
    {
        GetWeaponClass();
        GetPlayerController();
        // currentWeapon = GameObject.FindGameObjectWithTag("WeaponSprite").gameObject.GetComponentInChildren<WeaponClass>();
        //  ammoText.text = $"{currentWeapon.ammo}";

        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = $"{playerController.playerMoney}";
    }

    public void UpdateAmmo()
    {
        ammoText.text = $"{currentWeapon.ammo}";
    }

    public void GetPlayerController()
    {
        try
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.Log("Could't Find A Player Something Is Really Wrong");
        }
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
            ammoText.text = "0";
            Debug.Log("No Weapon Could Be Found");
        }
    }
}
