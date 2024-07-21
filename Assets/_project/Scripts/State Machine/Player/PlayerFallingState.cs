using UnityEngine;

public class PlayerFallingState : PlayerBaseState{
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    private Vector3 _momentum;

    public override void Enter() {
        _momentum = StateMachine.Controller.velocity;
        _momentum.y = 0;
        StateMachine.Animator.CrossFadeInFixedTime("Fall", 0.5f);
    }

    public override void Tick(float deltaTime) {
        Move(StateMachine, _momentum, deltaTime);
        FaceTarget(deltaTime);

        if(StateMachine.Controller.isGrounded){
            ReturnToLocomotion();
            return;
        }
    }

    public override void Exit() { }
    public override string ToString() { return "Falling"; }
}