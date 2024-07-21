using UnityEngine;

public class PlayerDeadState : PlayerBaseState{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        StateMachine.Animator.CrossFadeInFixedTime("Death", 0.5f);
    }
    
    public override void Exit() {}

    public override void Tick(float deltaTime) {
        StateMachine.WeaponDamage.gameObject.SetActive(false);
    }

    public override string ToString() {
        return "Dead";
    }
}