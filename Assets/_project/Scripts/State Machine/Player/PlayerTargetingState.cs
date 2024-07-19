using System;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    public readonly int TargetingFowardHash = Animator.StringToHash("TargetingFoward");
    public readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    public override void Enter(){
        StateMachine.InputReader.CancelEvent += OnCancel;
        StateMachine.Animator.Play(TargetingBlendTree);
    }

    public override void Tick(float deltaTime){
        if(StateMachine.Targeter.CurrentTarget == null){
            StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
            return;
        }

        FaceTarget(deltaTime);
        UpdateAnimator(deltaTime);

        if(StateMachine.InputReader.MovementValue == Vector2.zero){return;}
        Move(StateMachine.TargetingMovementSpeed * CalculateMovement(), deltaTime);
    }

    public override void Exit(){
        StateMachine.InputReader.CancelEvent += OnCancel;
    }

    private void OnCancel(){
        StateMachine.Targeter.Cancel();
        StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
    }

    private void UpdateAnimator(float deltaTime){
        if(StateMachine.InputReader.MovementValue.y == 0){
            StateMachine.Animator.SetFloat(TargetingFowardHash, 0, 0.1f, deltaTime);
        }else{
            float value = StateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            StateMachine.Animator.SetFloat(TargetingFowardHash, value, 0.1f, deltaTime);
        }

        if(StateMachine.InputReader.MovementValue.x == 0){
            StateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }else{
            float value = StateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            StateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }
    }

    private Vector3 CalculateMovement(){
        Vector3 movement = new();

        movement += StateMachine.transform.right * StateMachine.InputReader.MovementValue.x;
        movement += StateMachine.transform.forward * StateMachine.InputReader.MovementValue.y;

        return movement;
    }


    public override string ToString(){
        return "Targeting";
    }
}