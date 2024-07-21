using UnityEngine;

public class EnemyImpactState : EnemyBaseState{
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine){}
    public readonly int ImpactHash = Animator.StringToHash("Impact");
    private float _duration = 1f;

    public override void Enter(){
        StateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.05f);
    }

    public override void Tick(float deltaTime){
        Move(StateMachine, deltaTime);

        _duration -= deltaTime;
        if(_duration <= 0){
            StateMachine.SwitchState(StateMachine.EnemyStates.Idle);
        }
    }

    public override void Exit(){
        _duration = 1f;
    }
}