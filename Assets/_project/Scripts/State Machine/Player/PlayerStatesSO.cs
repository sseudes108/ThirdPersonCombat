using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStates", menuName = "3dPersonCombat/State Machine/PlayerStates")]
public class PlayerStateSO : ScriptableObject {
    public PlayerStateMachine StateMachine {get; private set;}
    public PlayerFreeLookState FreeLook {get; private set;}
    public PlayerTargetingState Targeting {get; private set;}

    public void SetStateMachine(PlayerStateMachine stateMachine){
        StateMachine = stateMachine;
    }

    public void CreateStates(){
        FreeLook = new PlayerFreeLookState(StateMachine);
        Targeting = new PlayerTargetingState(StateMachine);
    }
}