using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStates", menuName = "3dPersonCombat/State Machine/PlayerStates")]
public class PlayerStatesSO : StatesSO {
    public PlayerStateMachine StateMachine {get; private set;}
    public PlayerFreeLookState FreeLook {get; private set;}
    public PlayerTargetingState Targeting {get; private set;}
    public PlayerAttackingState Attacking {get; private set;}

    public override void SetStateMachine(StateMachine stateMachine){
        StateMachine = stateMachine as PlayerStateMachine;
    }

    public override void CreateStates(){
        FreeLook = new PlayerFreeLookState(StateMachine);
        Targeting = new PlayerTargetingState(StateMachine);
        Attacking = new PlayerAttackingState(StateMachine);
    }

}