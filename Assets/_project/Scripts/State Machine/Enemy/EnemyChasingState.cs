using UnityEngine;

public class EnemyChasingState : EnemyBaseState{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine){}
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    
    public override void Enter(){
        StateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime){
        if(StateMachine.Player.Health.IsDead) {
            StateMachine.Player = null;
            StateMachine.SwitchState(StateMachine.EnemyStates.Idle);
        }
        
        if(!IsInChaseRange()){
            StateMachine.SwitchState(StateMachine.EnemyStates.Idle);
            return;
        }

        MoveToPlayer(deltaTime);
        FacePlayer(deltaTime);

        if(IsInAttackRange()){
            StateMachine.SwitchState(StateMachine.EnemyStates.Attacking);
            return;
        }

        StateMachine.Animator.SetFloat(SpeedHash, 1, 0.1f, deltaTime);
    }

    public override void Exit(){
        if(StateMachine.NavMeshAgent.isOnNavMesh){
            StateMachine.NavMeshAgent.ResetPath();
            StateMachine.NavMeshAgent.velocity = Vector3.zero;
        }
    }

    private void MoveToPlayer(float deltaTime){
        if(StateMachine.NavMeshAgent.isOnNavMesh){
            StateMachine.NavMeshAgent.destination = StateMachine.Player.transform.position;
            Move(StateMachine, StateMachine.NavMeshAgent.desiredVelocity.normalized * StateMachine.MovementSpeed, deltaTime);
        }

        StateMachine.NavMeshAgent.velocity = StateMachine.Controller.velocity;
    }

    private bool IsInAttackRange(){
        float playerDistanceSqr = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr < StateMachine.AttackRange * StateMachine.AttackRange;
    }
}