using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputSystem_Actions.IPlayerActions{
    public Vector2 MovementValue {get; private set;}
    public bool IsAttacking {get; private set;}
    public bool IsBlocking {get; private set;}

    public event Action TargetEvent;
    public event Action CancelEvent;
    public event Action DodgeEvent;
    public event Action JumpEvent;

    private InputSystem_Actions InputActions;

    private void OnEnable() {
        InputActions.Player.Enable();
    }

    private void OnDisable() {
        InputActions.Player.Disable();
    }

    private void Awake() {
        InputActions = new();
        InputActions.Player.SetCallbacks(this);
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(context.performed){
            IsAttacking = true;
            return;
        }
        IsAttacking = false;
    }

    public void OnCancel(InputAction.CallbackContext context){
        if(!context.performed){return;}
        CancelEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context){
        if(!context.performed){return;}
        DodgeEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context){}
    public void OnJump(InputAction.CallbackContext context){
        if(!context.performed){return;}
        JumpEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context){}

    public void OnMove(InputAction.CallbackContext context){
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context){}

    public void OnTarget(InputAction.CallbackContext context){
        if(!context.performed){return;}
        TargetEvent?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context){
        if(context.performed){
            IsBlocking = true;
            return;
        }
        IsBlocking = false;
    }

    public void ResetActions(){
        IsBlocking = false;
        IsBlocking = false;
    }
}