using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputSystem_Actions.IPlayerActions{
    public Vector2 MovementValue {get; private set;}
    public bool IsAttacking {get; private set;}
    public event Action TargetEvent;
    public event Action CancelEvent;

    private InputSystem_Actions InputActions;

    private void Awake() {
        InputActions = new();
        InputActions.Player.SetCallbacks(this);

        InputActions.Player.Enable();
    }

    private void OnDestroy() {
        InputActions.Player.Disable();
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

    public void OnDodge(InputAction.CallbackContext context){}
    public void OnInteract(InputAction.CallbackContext context){}
    public void OnJump(InputAction.CallbackContext context){}
    public void OnLook(InputAction.CallbackContext context){}

    public void OnMove(InputAction.CallbackContext context){
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context){}

    public void OnTarget(InputAction.CallbackContext context){
        if(!context.performed){return;}
        TargetEvent?.Invoke();
    }
}