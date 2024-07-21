using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStates", menuName = "3dPersonCombat/State Machine/PlayerStates")]
public class PlayerStatesSO : StatesSO {
    public PlayerStateMachine StateMachine {get; private set;}
    public PlayerFreeLookState FreeLook {get; private set;}
    public PlayerTargetingState Targeting {get; private set;}
    public PlayerAttackingState Attacking {get; private set;}
    public PlayerImpactState Impact {get; private set;}
    public PlayerDeadState Dead {get; private set;}
    public PlayerBlockingState Blocking {get; private set;}
    public PlayerJumpingState Jumping {get; private set;}
    public PlayerFallingState Falling {get; private set;}
    public PlayerDodgingState Dodging {get; private set;}


    public override void SetStateMachine(StateMachine stateMachine){
        StateMachine = stateMachine as PlayerStateMachine;
    }

    public override void CreateStates(){
        FreeLook = new PlayerFreeLookState(StateMachine);
        Targeting = new PlayerTargetingState(StateMachine);
        Attacking = new PlayerAttackingState(StateMachine);
        Impact = new PlayerImpactState(StateMachine);
        Dead = new PlayerDeadState(StateMachine);
        Blocking = new PlayerBlockingState(StateMachine);
        Jumping = new PlayerJumpingState(StateMachine);
        Falling = new PlayerFallingState(StateMachine);
        Dodging = new PlayerDodgingState(StateMachine);
    }

}