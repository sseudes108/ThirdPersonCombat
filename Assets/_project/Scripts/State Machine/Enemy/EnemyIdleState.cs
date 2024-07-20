using UnityEngine;

public class EnemyIdleState : EnemyBaseState{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter(){
        Debug.Log("Enter Enemy Idle State");
    }

    public override void Tick(float deltaTime){}

    public override void Exit(){}
}