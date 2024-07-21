using UnityEngine;

public class EnemyDeadState : EnemyBaseState{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        Debug.Log("Enemy Dead");
        StateMachine.Animator.CrossFadeInFixedTime("Death", 0.5f);
    }

    public override void Tick(float deltaTime) {
        StateMachine.WeaponDamage.gameObject.SetActive(false);
        GameObject.Destroy(StateMachine.Target);
    }

    public override void Exit() {}
}