using UnityEngine;

public abstract class StatesSO : ScriptableObject {
    public abstract void SetStateMachine(StateMachine stateMachine);
    public abstract void CreateStates();
}