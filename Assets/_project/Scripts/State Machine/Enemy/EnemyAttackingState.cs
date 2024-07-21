using UnityEngine;

public class EnemyAttackingState : EnemyBaseState{
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine){}
    public readonly int AttackHash = Animator.StringToHash("Attack");

    public override void Enter(){
        StateMachine.WeaponDamage.SetDamage(StateMachine.Attacks[0].AttackDamage, StateMachine.Attacks[0].KnockbackForce);
        StateMachine.Animator.CrossFadeInFixedTime(AttackHash, 0.05f);
    }

    public override void Tick(float deltaTime){
        if(GetNormalizedTime(StateMachine.Animator) >= 1){
            StateMachine.SwitchState(StateMachine.EnemyStates.Chasing);
        }
        FacePlayer(deltaTime);
    }

    public override void Exit(){}

}