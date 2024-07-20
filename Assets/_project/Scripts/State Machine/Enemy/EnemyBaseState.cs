public abstract class EnemyBaseState : State {
    public EnemyBaseState(EnemyStateMachine stateMachine){
        StateMachine = stateMachine;
    }

    public EnemyStateMachine StateMachine { get; private set; }
}