using UnityEngine;

public class EnemyIdleState : EnemyBaseState{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void Enter(){
        StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime){
        Move(StateMachine, deltaTime);

        if(IsInChaseRange()){
            StateMachine.SwitchState(StateMachine.EnemyStates.Chasing);
            return;
        }

        StateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
    }

    public override void Exit(){}
}