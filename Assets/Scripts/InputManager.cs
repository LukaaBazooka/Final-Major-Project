using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions OnFoot;

    public InventoryManager inventoryManager;

    private PlayerMotor motor;
    private PlayerLook look;

    [SerializeField] GameObject inventoryParent;


    bool IsInventoryOpeened;
    void Update()
    {

       

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsInventoryOpeened)
            {
                IsInventoryOpeened = false;
            }
            else
            {

                IsInventoryOpeened = true;

            }
        }
    }


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = new PlayerInput();
        OnFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        OnFoot.Jump.performed += ctx => motor.Jump();

        look = GetComponent<PlayerLook>();

        OnFoot.Crouch.performed += ctx => motor.Crouch();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the valye from our movement action
        motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        if (!IsInventoryOpeened)
        {
            look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
        }

    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }
    private void OnDisable()
    {
        OnFoot.Disable();
    }

}
