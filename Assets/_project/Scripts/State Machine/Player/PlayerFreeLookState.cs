using System.Collections;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}
    public readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");
    public readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");

    public override void Enter(){
        StateMachine.InputReader.TargetEvent += OnTarget;
        StateMachine.InputReader.JumpEvent += OnJump;
        StateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTree, 0.05f);
        StateMachine.StartCoroutine(RemoveTargetFromCameraGroup());
    }

    public override void Tick(float deltaTime){ 
        if(StateMachine.InputReader.IsAttacking){
            StateMachine.SwitchState(StateMachine.PlayerStates.Attacking);
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(StateMachine, movement * StateMachine.FreelookMovementSpeed, deltaTime);

        if(StateMachine.InputReader.MovementValue == Vector2.zero){
            StateMachine.Animator.SetFloat(FreeLookSpeed, 0, 0.1f, deltaTime);
            return;
        }

        StateMachine.Animator.SetFloat(FreeLookSpeed, 1, 0.1f, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit(){
        StateMachine.InputReader.TargetEvent -= OnTarget;
        StateMachine.InputReader.JumpEvent -= OnJump;
    }

    private Vector3 CalculateMovement(){
        var forward = StateMachine.MainCameraPosition.forward;
        var right = StateMachine.MainCameraPosition.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * StateMachine.InputReader.MovementValue.y + right * StateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime){
        StateMachine.transform.rotation = Quaternion.Lerp(
            StateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * StateMachine.RotationSmoothValue
        );
    }

    public void OnTarget(){
        if(!StateMachine.Targeter.SelectTarget()){return;}
        StateMachine.SwitchState(StateMachine.PlayerStates.Targeting);
    }

    private void OnJump(){
        StateMachine.SwitchState(StateMachine.PlayerStates.Jumping);
    }

    public override string ToString(){
        return "Free Look";
    }

    private IEnumerator RemoveTargetFromCameraGroup(){
        yield return new WaitForSeconds(1f);
        StateMachine.Targeter.RemoveT();
    }
}