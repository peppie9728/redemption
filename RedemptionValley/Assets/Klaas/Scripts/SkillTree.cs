using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SkillTree : MonoBehaviour
{
    public static event HandleUIHealthChange OnPointsChange;
    public delegate void HandleUIHealthChange();

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
    [Header("Gamemanager")]
    [SerializeField] private GameManager gameManager;
    /*
     * to be able to upgrade a skill the currentskillamount must exceed the skill requirement of the skill the player want to buy and have enough skill points
     */

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindAnyObjectByType<EventSystem>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.isSkillTreeOpen = true;
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
        gameManager.isSkillTreeOpen = false;
        skillBackground.gameObject.SetActive(false);
        playerController.enabled = true;
    }

    [Header("Health Uograde Info")]
    [SerializeField] private float healthCost = 1;
    [SerializeField] private TextMeshProUGUI healthText;
    public void UpgradeMaxHealth()
    {
        if (playerController.playerSkillPoints >= healthCost)
        {
            playerController.playerSkillPoints -= (Int32)healthCost;
            OnPointsChange?.Invoke();

            float newValue = playerController.playerHealthBar.maxValue / 1.5f * 2f;
            newValue = Mathf.Round(newValue);

            playerController.playerHealthBar.maxValue = newValue;
            playerController.SetHealth(newValue);

            healthCost = healthCost / 3 * 5;
            healthCost = Mathf.Round(healthCost);
            healthText.text = healthCost.ToString();

        }
        else
        {
            Debug.LogWarning($"Not Enough Points Need:{healthCost} - Current Points: {playerController.playerSkillPoints}");
        }
        Debug.LogWarning($"New Health: {playerController.GetHealth()} - HealthBar MaValue: {playerController.playerHealthBar.maxValue}");

    }

    [Header("Melee Upgrade Info")] // not done yet
    [SerializeField] private float meleeCost = 1;
    [SerializeField] private TextMeshProUGUI meleeText;
    public void UpgradeMelee()
    {
        if (playerController.playerSkillPoints >= meleeCost)
        {
            //playerMeleeClass.attackSpeed *= 2;
            OnPointsChange?.Invoke();
            //playerMeleeClass.meleeDamage *= 2;
            playerMeleeClass.UpgradeMeleeDamage();
            currentSkillAmount++;
            playerController.playerSkillPoints -= (Int32)meleeCost;
            OnPointsChange?.Invoke();
            meleeCost = meleeCost / 3 * 5;
            meleeCost = Mathf.Round(meleeCost);

            meleeText.text = meleeCost.ToString();
        }
    }

    [Header("Stamine Upgrade Info")]
    [SerializeField] private float stamineCost = 1;
    [SerializeField] private TextMeshProUGUI stamineText;
    public void UpgradeMaxStamina()
    {

        if (playerController.playerSkillPoints >= stamineCost)
        {

            playerController.playerSkillPoints -= (Int32)stamineCost;
            OnPointsChange?.Invoke();

            float newValue = playerController.playerStaminaSlider.maxValue / 3 * 5;
            newValue = MathF.Round(newValue);

            playerController.playerStaminaSlider.maxValue = newValue;
            playerController.stamina = newValue;

            stamineCost = stamineCost / 3 * 5;
            stamineCost = MathF.Round(stamineCost);

            stamineText.text = stamineCost.ToString();

        }
        else
        {
            Debug.LogWarning($"Not Enough Points Need:{stamineCost} - Current Points: {playerController.playerSkillPoints}");
        }

        Debug.LogWarning($"Current Max Slider Value: {playerController.playerStaminaSlider.maxValue} - Current Player Stamina: {playerController.stamina}");
    }


    [Header("Gun Upgrade Info")]
    [SerializeField] private float gunCost = 1;
    [SerializeField] private TextMeshProUGUI gunText;
    public void UpgradeGunDamage()
    {
        if (playerController.playerSkillPoints >= gunCost)
        {

            playerController.playerSkillPoints -= (Int32)gunCost;
            OnPointsChange?.Invoke();

            float newValue = currentPlayerWeapon.damage / 3 * 5;
            newValue = MathF.Round(newValue);

            currentPlayerWeapon.damage = (Int32)newValue;

            gunCost = gunCost / 3 * 5;
            gunCost = MathF.Round(gunCost);

            gunText.text = gunCost.ToString();
        }
    }
    [Header("Money Gain Upgrade")]
    [SerializeField] private float moneyCost = 1;
    [SerializeField] private TextMeshProUGUI moneyText;
    public void UpgradeMonayGain()
    {
        if(playerController.playerSkillPoints >= moneyCost)
        {
            playerController.playerSkillPoints -= (Int32)moneyCost;
            OnPointsChange?.Invoke();

            float newValue = gameManager.moneyReward / 3;
            newValue = MathF.Round(newValue);

            gameManager.moneyReward += (Int32)newValue;

            moneyCost = moneyCost / 3 * 5;
            moneyCost = MathF.Round(moneyCost);

            moneyText.text = moneyCost.ToString(); 
        }

    }


    [Header("Skill Point Gain")]
    [SerializeField] private float skillCost = 8;
    [SerializeField] private TextMeshProUGUI skillText;
    public void UpgradeSkillPointGain()
    {
        if(playerController.playerSkillPoints >= skillCost)
        {
            playerController.playerSkillPoints -= (Int32)skillCost;
            OnPointsChange?.Invoke();

            float newValue = gameManager.skillPointReward / 3 * 6;
            newValue = MathF.Round(newValue);

            gameManager.skillPointReward = (Int32)newValue;

            skillCost = skillCost / 3 * 5;
            skillCost = MathF.Round(skillCost);

            skillText.text = skillCost.ToString();
        }
    }
}
