using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;

    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    public Vector2 MovementValue { get; private set; }

    public event Action OnJumpEvent;
    public event Action OnDodgeEvent;
    public event Action OnTargetEvent;  

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnJumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnDodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnTargetEvent?.Invoke();
    }   

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            IsAttacking = true;
        else if (context.canceled)
            IsAttacking = false;
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
            IsBlocking = true;
        else if (context.canceled)
            IsBlocking = false;
    }
}
