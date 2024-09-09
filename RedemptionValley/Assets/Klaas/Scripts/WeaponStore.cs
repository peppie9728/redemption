using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WeaponStore : MonoBehaviour
{
    public static event HandleUIAmmoChange OnAmmoAdd;
    public delegate void HandleUIAmmoChange();

    [Header("Players Controller")]
    [SerializeField] private PlayerController playerController;

    [Header("Players Current Weapon")]
    [SerializeField] private WeaponClass currentWeaponClass;

    [Header("UI Elements")]
    [SerializeField] private Image storeBackground;
    [SerializeField] private GameObject BuyAmmoButton;

    [Header("EventSystem")]
    [SerializeField] EventSystem eventSystem;

    public void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision");
            currentWeaponClass = collision.gameObject.GetComponentInChildren<WeaponClass>();   
            Debug.Log($"Current Weapon:{currentWeaponClass.weaponName}");

            storeBackground.gameObject.SetActive(true);
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.enabled = false;

            eventSystem.firstSelectedGameObject = BuyAmmoButton;
        }
    }
    
    public void CloseShop()
    {
        storeBackground.gameObject.SetActive(false);
        playerController.enabled = true;
    }

    public void BuyAmmo()
    {
        currentWeaponClass.ammo += 32;
        OnAmmoAdd?.Invoke();
        //Add Ammo Price
    }

    public void Heal()
    {
        playerController.AddHealth(10);
        //Add Price For Healing
        //Add Info Box on select Saying what it will do
    }

}
