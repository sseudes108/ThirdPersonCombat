using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine {
    [field:SerializeField] public EnemyStatesSO EnemyStates { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set;}
    public PlayerStateMachine Player;

    [field:SerializeField] public float MovementSpeed { get; private set; }
    [field:SerializeField] public float PlayerDetectionRange { get; private set; }
    [field:SerializeField] public float AttackRange { get; private set; }

    public Target Target { get; private set; }

    public float RotationSmoothValue  { get; private set; } = 10f;
    
    public override void Awake() {
        base.Awake();
        Player = FindFirstObjectByType<PlayerStateMachine>();
        Target = GetComponent<Target>();
    }

    private void Start() {
        NavMeshAgent.updatePosition = false; // block the change from Nav Agent
        NavMeshAgent.updateRotation = false; // block the change from Nav Agent
        SwitchState(EnemyStates.Idle);
    }

    public override void SetStates(){
        EnemyStates.SetStateMachine(this);
        EnemyStates.CreateStates();
    }

    public override void SetComponents(){
        base.SetComponents();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Target = GetComponent<Target>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public override void OnDamageTaken(){
        SwitchState(EnemyStates.Impact);
    }

    public override void OnDie(){
        SwitchState(EnemyStates.Dead);
    }
}