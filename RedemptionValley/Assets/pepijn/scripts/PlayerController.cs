using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    }

    public void AddHealth(float input)
    {
        health += input;
    }


}
