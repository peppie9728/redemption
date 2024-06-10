using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("Health Bar")]
    public Slider playerHealthBar;
    //this is the player controllerr everything that the player can do happens here.

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
    public KeyCode[] playerInput;
    private KeyCode input;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
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
            stamina -= staminaLoss * Time.deltaTime;
        }
        else
        {
            rb.velocity = new Vector2(inputHorizontal * moveSpeed * Time.fixedDeltaTime, inputVertical * moveSpeed * Time.fixedDeltaTime);
        }
        if (stamina < 10 && !isSprinting)
        {
            stamina += staminaLoss * Time.deltaTime;
        }

        playerAnimator.SetFloat("x", inputHorizontal);
        playerAnimator.SetFloat("y", inputVertical);
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



}
