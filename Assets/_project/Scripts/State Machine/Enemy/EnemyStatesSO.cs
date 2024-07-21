using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStates", menuName = "3dPersonCombat/State Machine/EnemyStates")]
public class EnemyStatesSO : StatesSO{
    public EnemyStateMachine StateMachine {get; private set;}

    public EnemyIdleState Idle {get; private set;}
    public EnemyChasingState Chasing {get; private set;}
    public EnemyAttackingState Attacking {get; private set;}
    public EnemyImpactState Impact {get; private set;}
    public EnemyDeadState Dead {get; private set;}

    public override void SetStateMachine(StateMachine stateMachine){
        StateMachine = stateMachine as EnemyStateMachine;
    }

    public override void CreateStates(){
        Idle = new(StateMachine);
        Chasing = new(StateMachine);
        Attacking = new(StateMachine);
        Impact = new(StateMachine);
        Dead = new(StateMachine);
    }

}