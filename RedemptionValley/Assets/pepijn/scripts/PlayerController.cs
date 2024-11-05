using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour
{
    public static event HandleUIUpdate UpdateUI;
    public delegate void HandleUIUpdate(ItemType itemType);

    [Header("Health Bar")]
    public Slider playerHealthBar;
    [Header("Stamina Bar")]
    public Slider playerStaminaSlider;
    [Header("Economy")]
    public int playerMoney;
    public int playerSkillPoints;
    //this is the player controllerr everything that the player can do happens here.
    [Header("Weapon")]
    public WeaponClass playerWeaponClass;

    [Header("player variables")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float sprintSpeed;
    [SerializeField]
    float health;
    [SerializeField]
    float stamina;
    [SerializeField]
    float staminaLoss;
    [SerializeField]
    string horizontalName;
    [SerializeField]
    string verticalName;
    [SerializeField]
    string sprintButton;

    Rigidbody2D rb;
    bool isSprinting;

    float inputHorizontal;
    float inputVertical;
    //inventory

    [Header("Player Movement Animation")]
    public Animator playerAnimator;

    [Header("Player Attack")]
    public MeleeClass meleeClass;

    private void OnEnable()
    {
        PickUps.OnPickUp += ItemPickUp;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeClass = gameObject.GetComponent<MeleeClass>();
    }
    private void Update()
    {
        HandleMovement();
    }

    public void FixedUpdate()
    {
        Move();
       
    }

    public void Move()
    {

        if (isSprinting && stamina > 0)
        {
            rb.velocity = new Vector2(inputHorizontal * sprintSpeed * Time.fixedDeltaTime,inputVertical * sprintSpeed * Time.fixedDeltaTime);
            playerStaminaSlider.value = stamina;
            stamina -= staminaLoss * Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(inputHorizontal * moveSpeed * Time.fixedDeltaTime, inputVertical * moveSpeed * Time.fixedDeltaTime);
        }
        if (stamina < 10 && !isSprinting)
        {
            stamina += staminaLoss * Time.deltaTime;
            playerStaminaSlider.value = stamina;
        }



        playerAnimator.SetFloat("x", inputHorizontal);
        playerAnimator.SetFloat("y", inputVertical);

        playerAnimator.SetFloat("attackCountdown", meleeClass.attackDelay);
        
    }

    public void HandleMovement()
    {
        inputHorizontal = Input.GetAxisRaw(horizontalName);
        inputVertical = Input.GetAxisRaw(verticalName);
        if (Input.GetKey(sprintButton))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float input)
    {
        health = input;
        playerHealthBar.value = health;
    }

    public void AddHealth(float input)
    {
        health += input;
        playerHealthBar.value = health;
    }

    public void GetCurrentWeapon()
    {  
            playerWeaponClass = gameObject.GetComponentInChildren<WeaponClass>();
    }
    public void ItemPickUp(int amountPickedUp, ItemType itemType)
    {
        //Debug.LogWarning($"Item Type Found:{itemType}");
        switch (itemType)
        {
            case ItemType.Gold:
                playerMoney += amountPickedUp;
                UpdateUI?.Invoke(itemType);
                break;
            case ItemType.Ammo:
               
                if (playerWeaponClass == null)
                {
                    GetCurrentWeapon();
                }
                playerWeaponClass.ammo += amountPickedUp;
                UpdateUI?.Invoke(itemType);
                //Debug.Log("No Refrence To The Current Weapon Yet!!");
                break;
            case ItemType.SkillPoint:
                playerSkillPoints += amountPickedUp;
                UpdateUI?.Invoke(itemType);
                break;
            default:
                break;
        }
    }

}
