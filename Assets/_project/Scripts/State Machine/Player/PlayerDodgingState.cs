using UnityEngine;

public class PlayerDodgingState : PlayerBaseState{
    public PlayerDodgingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public readonly int DodgeBlendTree = Animator.StringToHash("DodgeBlendTree");
    public readonly int DodgingFowardHash = Animator.StringToHash("DodgingForward");
    public readonly int DodgingRightHash = Animator.StringToHash("DodgingRight");

    private float _remainingDodgeTime;
    Vector2 _direction;

    public override void Enter() {
        StateMachine.Health.SetInvulnerable(true);
        _remainingDodgeTime = StateMachine.DodgeDuration;

        StateMachine.Animator.SetFloat(DodgingFowardHash, StateMachine.InputReader.MovementValue.y);
        StateMachine.Animator.SetFloat(DodgingRightHash, StateMachine.InputReader.MovementValue.y);
        _direction.x = StateMachine.InputReader.MovementValue.x;
        _direction.y = StateMachine.InputReader.MovementValue.y;

        StateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTree, 0.1f);
    }

    public override void Tick(float deltaTime) {
        Vector3 movement = new();

        movement += _direction.x * StateMachine.DodgeLenght * StateMachine.transform.right / StateMachine.DodgeDuration;
        movement += _direction.y * StateMachine.DodgeLenght * StateMachine.transform.forward / StateMachine.DodgeDuration;

        Move(StateMachine, movement, deltaTime);

        FaceTarget(deltaTime);

        _remainingDodgeTime -= deltaTime;
        if(_remainingDodgeTime <= 0 ){
            ReturnToLocomotion();
        }
    }

    public override void Exit() {
        StateMachine.Health.SetInvulnerable(false);
    }

    public override string ToString() { return "Dodging"; }
}