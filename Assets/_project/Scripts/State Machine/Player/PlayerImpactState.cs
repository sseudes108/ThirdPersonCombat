using UnityEngine;

public class PlayerImpactState : PlayerBaseState{
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine){}
    public readonly int ImpactHash = Animator.StringToHash("Impact");
    private float _duration = 1f;

    public override void Enter(){
        ResetAttackIndex();
        if(StateMachine.InputReader.IsBlocking) { StateMachine.Animator.CrossFadeInFixedTime("BlockingImpact", 0.05f); }
        StateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.05f);
    }

    public override void Tick(float deltaTime){
        Move(StateMachine, deltaTime);
        
        _duration -= deltaTime;
        if(_duration <= 0){
            ReturnToLocomotion();
        }
    }

    public override void Exit(){
        _duration = 1f;
    }

    public override string ToString(){
        return "Impact";
    }
}