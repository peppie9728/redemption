using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillTree : MonoBehaviour
{
    [Header("Player")]
    public PlayerController playerController;
    [SerializeField] private MeleeClass playerMeleeClass;
    [SerializeField] private WeaponClass currentPlayerWeapon;

    [Header("SKill's Bought")]
    public int currentSkillAmount = 0;

    [Header("Buttons")]
    [SerializeField] private Button[] skillButtons;

    [Header("UI")]
    [SerializeField] private Image skillBackground;

    [Header("EventSystem")]
    [SerializeField] private EventSystem eventSystem;
    /*
     * to be able to upgrade a skill the currentskillamount must exceed the skill requirement of the skill the player want to buy and have enough skill points
     */

    // Start is called before the first frame update
    void Start()
    {
       eventSystem = GameObject.FindAnyObjectByType<EventSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            eventSystem.SetSelectedGameObject(skillButtons[0].gameObject);
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerMeleeClass = collision.gameObject.gameObject.GetComponent<MeleeClass>();
            currentPlayerWeapon = collision.gameObject.gameObject.GetComponent<WeaponClass>();

            skillBackground.gameObject.SetActive(true);
            playerController.StopAllMovement();
            playerController.enabled = false;

        }
    }
    
    public void CloseSkillTree()
    {
        skillBackground.gameObject.SetActive(false);
        playerController.enabled = true;
    }

    [Header("Health Uograde Info")]
    [SerializeField] private float healthCost = 1;
    public void UpgradeMaxHealth()
    {     
        if(playerController.playerSkillPoints >= healthCost)
        {
            playerController.playerSkillPoints -= (Int32)healthCost;

            float newValue = playerController.playerHealthBar.maxValue  / 1.5f * 2f;
            newValue = Mathf.Round(newValue);

            playerController.playerHealthBar.maxValue = newValue;
            playerController.SetHealth(newValue);

            healthCost = healthCost / 3 * 5;
            healthCost = Mathf.Round(healthCost);

        } 
        else
        {
           Debug.LogWarning($"Not Enough Points Need:{healthCost} - Current Points: {playerController.playerSkillPoints}");
        }
         Debug.LogWarning($"New Health: {playerController.GetHealth()} - HealthBar MaValue: {playerController.playerHealthBar.maxValue}");

    }

    [Header("Melee Upgrade Info")] // not done yet
    [SerializeField] private float meleeCost = 1;
    public void UpgradeMelee(int cost)
    {
        if (playerController.playerSkillPoints >= cost)
        {
            //playerMeleeClass.attackSpeed *= 2;

            playerMeleeClass.meleeDamage *= 2;

            currentSkillAmount++;
            playerController.playerSkillPoints -= cost;

            cost *= 2;
        }
    }

    [Header("Stamine Upgrade Info")]
    [SerializeField] private float stamineCost = 1;
    public void UpgradeMaxStamina()
    {
       
        if (playerController.playerSkillPoints >= stamineCost)
        {
            playerController.playerSkillPoints -= (Int32)stamineCost;

            float newValue = playerController.playerStaminaSlider.maxValue / 3 * 5;
            newValue = MathF.Round(newValue);

            playerController.playerStaminaSlider.maxValue = newValue;
            playerController.stamina = newValue;

            stamineCost = stamineCost / 3 * 5;
            stamineCost = MathF.Round(stamineCost);
            
        } 
        else
        {
            Debug.LogWarning($"Not Enough Points Need:{stamineCost} - Current Points: {playerController.playerSkillPoints}");
        }

         Debug.LogWarning($"Current Max Slider Value: {playerController.playerStaminaSlider.maxValue} - Current Player Stamina: {playerController.stamina}");
    }

}
