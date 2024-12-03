using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WeaponStore : MonoBehaviour
{
    public static event HandleUIAmmoChange OnAmmoAdd;
    public delegate void HandleUIAmmoChange();

    public static event HandleUIHealthChange OnMoneyChange;
    public delegate void HandleUIHealthChange();

    [Header("Players Controller")]
    [SerializeField] private PlayerController playerController;

    [Header("Players Current Weapon")]
    [SerializeField] private WeaponClass currentWeaponClass;
    
    [Header("UI Elements")]
    [SerializeField] private Image storeBackground;
    [SerializeField] private GameObject BuyAmmoButton;

    [Header("Upgrade Buttons")]
    [SerializeField] private Button uprgadeOneB;
    [SerializeField] private Button uprgadeTwoB;
    [SerializeField] private TextMeshProUGUI upgradeOneT;
    [SerializeField] private TextMeshProUGUI upgradeTwoT;

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
            playerController.StopAllMovement();
            playerController.enabled = false;

            SetUpgradeText();

            eventSystem.SetSelectedGameObject(BuyAmmoButton);
        }
    }
    
    public void CloseShop()
    {
        storeBackground.gameObject.SetActive(false);
        playerController.enabled = true;
    }

    public void BuyAmmo()
    {
        if (playerController.playerMoney >= 15)
        {
            currentWeaponClass.ammo += 32;
            playerController.playerMoney -= 15;
            OnAmmoAdd?.Invoke();
            OnMoneyChange?.Invoke();
        }
        else
        {

        }
        //Add Ammo Price
    }

    public void Heal()
    {
        if (playerController.playerMoney >= 10 && playerController.playerHealthBar.value < playerController.playerHealthBar.maxValue)
        {
            playerController.AddHealth(10);
            playerController.playerMoney -= 10;
            OnMoneyChange?.Invoke();
        } 
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    

    public void UpgradeOne()
    {
        if (currentWeaponClass.currentUpgrade == CurrentUpgrade.Basic)
        {
            eventSystem.SetSelectedGameObject(BuyAmmoButton);
            currentWeaponClass.UpgradeOne();

            uprgadeOneB.interactable = false;
            uprgadeTwoB.interactable = false;
        }
        else { Debug.Log("Already Bought"); }
    }
    public void UprgadeTwo()
    {
        if (currentWeaponClass.currentUpgrade == CurrentUpgrade.Basic)
        {
            
            eventSystem.SetSelectedGameObject(BuyAmmoButton);
            // eventSystem.currentSelectedGameObject = BuyAmmoButton; 
            currentWeaponClass.UpgradeTwo();
            uprgadeTwoB.interactable = false;
            uprgadeOneB.interactable = false;
        } else { Debug.Log("Already Bought"); }
    }

    public void SetUpgradeText()
    {
        uprgadeOneB.GetComponentInChildren<TextMeshProUGUI>().text = currentWeaponClass.upNameOne;
        uprgadeTwoB.GetComponentInChildren<TextMeshProUGUI>().text = currentWeaponClass.upNameTwo;
    }

}
