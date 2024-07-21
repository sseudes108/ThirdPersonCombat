using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTargetingState : PlayerBaseState{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    public readonly int TargetingFowardHash = Animator.StringToHash("TargetingForward");
    public readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    public override void Enter(){
        StateMachine.InputReader.CancelEvent += OnCancel;
        StateMachine.InputReader.DodgeEvent += OnDodge;
        StateMachine.InputReader.JumpEvent += OnJump;
        StateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTree, 0.05f);
    }

    public override void Tick(float deltaTime){
        if(StateMachine.InputReader.IsAttacking){
            StateMachine.SwitchState(StateMachine.PlayerStates.Attacking);
            return;
        }

        if(StateMachine.Targeter.CurrentTarget == null){
            StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
            return;
        }

        FaceTarget(deltaTime);
        UpdateAnimator(deltaTime);

        if(StateMachine.InputReader.MovementValue == Vector2.zero){return;}
        Move(StateMachine, StateMachine.TargetingMovementSpeed * CalculateMovement(deltaTime), deltaTime);
    }

    public override void Exit(){
        StateMachine.InputReader.CancelEvent -= OnCancel;
        StateMachine.InputReader.DodgeEvent -= OnDodge;
        StateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump(){
        StateMachine.SwitchState(StateMachine.PlayerStates.Jumping);
    }

    private void OnCancel(){
        StateMachine.Targeter.Cancel();
        StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
    }

    private void OnDodge(){
        if(StateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        StateMachine.SwitchState(StateMachine.PlayerStates.Dodging);
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

    private Vector3 CalculateMovement(float deltaTime){
        Vector3 movement = new();

        // if(_remainingDodgeTime > 0){
        //     movement += _dodgeDirection.x * StateMachine.DodgeLenght * StateMachine.transform.right / StateMachine.DodgeDuration;
        //     movement += _dodgeDirection.y * StateMachine.DodgeLenght * StateMachine.transform.forward / StateMachine.DodgeDuration;

        //     _remainingDodgeTime -= deltaTime;
            
        //     if(_remainingDodgeTime < 0){
        //         _remainingDodgeTime = 0;
        //     }
        // }else{
        // }
        movement += StateMachine.transform.right * StateMachine.InputReader.MovementValue.x;
        movement += StateMachine.transform.forward * StateMachine.InputReader.MovementValue.y;

        return movement;
    }


    public override string ToString(){
        return "Targeting";
    }
}