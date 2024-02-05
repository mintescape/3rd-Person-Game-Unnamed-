using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public bool IsAttacking;
    public Vector2 MovementValue { get; private set; }
    private Controls controls;

    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) { JumpEvent?.Invoke(); }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed) { DodgeEvent?.Invoke(); }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnTargeting(InputAction.CallbackContext context)
    {
        if(context.performed) { TargetEvent?.Invoke(); }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) IsAttacking = true;
        //else if (context.canceled) IsAttacking = false;

    }
}
