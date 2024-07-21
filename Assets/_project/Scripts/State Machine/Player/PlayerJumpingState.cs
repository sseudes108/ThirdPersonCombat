using UnityEngine;

public class PlayerJumpingState : PlayerBaseState{
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    private Vector3 _momentum;

    public override void Enter() {
        StateMachine.ForceReceiver.Jump(StateMachine.JumpForce);
        _momentum = StateMachine.Controller.velocity;
        _momentum.y = 0;
        StateMachine.Animator.CrossFadeInFixedTime("Jump", 0.5f);
    }

    public override void Tick(float deltaTime) {
        Move(StateMachine, _momentum, deltaTime);
        FaceTarget(deltaTime);

        if(StateMachine.Controller.velocity.y <= 0){
            StateMachine.SwitchState(StateMachine.PlayerStates.Falling);
            return;
        }
    }

    public override void Exit() { }
    public override string ToString() { return "Jumping"; }
}