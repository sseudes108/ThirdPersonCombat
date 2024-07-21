public class PlayerBlockingState : PlayerBaseState{
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() {
        StateMachine.Animator.CrossFadeInFixedTime("Block", 0.1f);
    }

    public override void Tick(float deltaTime) {
        Move(StateMachine, deltaTime);
        
        if(!StateMachine.InputReader.IsBlocking){
            if(StateMachine.Targeter.CurrentTarget == null){
                StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
                return;
            }
            StateMachine.SwitchState(StateMachine.PlayerStates.Targeting);
        }
    }

    public override void Exit() {}

    public override string ToString() { return "Blocking"; }
}