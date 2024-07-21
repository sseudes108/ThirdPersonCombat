using UnityEngine;

public abstract class EnemyBaseState : State {
    public EnemyBaseState(EnemyStateMachine stateMachine){
        StateMachine = stateMachine;
    }

    public EnemyStateMachine StateMachine { get; private set; }

    protected bool IsInChaseRange(){
        if(StateMachine.Player == null) { return false; }

        float playerDistanceSqr = (StateMachine.Player.transform.position - StateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr < StateMachine.PlayerDetectionRange * StateMachine.PlayerDetectionRange;
    }

    protected void FacePlayer(float deltaTime){
        var lookPos = StateMachine.Player.transform.position - StateMachine.transform.position;
        lookPos.y = 0;
        StateMachine.transform.rotation = Quaternion.Lerp(
            StateMachine.transform.rotation,
            Quaternion.LookRotation(lookPos),
            deltaTime * StateMachine.RotationSmoothValue
        );
    }
}