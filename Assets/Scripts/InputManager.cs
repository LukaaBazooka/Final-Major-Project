using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions OnFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    void Awake()
    {
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
        look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());

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
