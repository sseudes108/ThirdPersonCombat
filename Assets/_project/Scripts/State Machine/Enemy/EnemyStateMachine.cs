using UnityEngine;

public class EnemyStateMachine : StateMachine {
    [field:SerializeField] public EnemyStatesSO EnemyStates { get; private set; }

    private void Awake() {
        SetComponents();
        SetStates();
    }

    private void Start() {
        SwitchState(EnemyStates.Idle);
    }

    private void SetStates(){
        EnemyStates.SetStateMachine(this);
        EnemyStates.CreateStates();
    }

    private void SetComponents(){
    }

}