using UnityEngine;

public abstract class PlayerBaseState : State {
    public PlayerBaseState(PlayerStateMachine stateMachine){
        StateMachine = stateMachine;
    }
    protected PlayerStateMachine StateMachine;

    public abstract override string ToString();

    protected void Move(Vector3 motion, float deltaTime){
        StateMachine.Controller.Move((motion +  StateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget(float deltaTime){
        if(StateMachine.Targeter.CurrentTarget == null){return;}
        var lookPos = StateMachine.Targeter.CurrentTarget.transform.position - StateMachine.transform.position;
        lookPos.y = 0;
        StateMachine.transform.rotation = Quaternion.Lerp(
            StateMachine.transform.rotation,
            Quaternion.LookRotation(lookPos),
            deltaTime * StateMachine.RotationSmoothValue
        );
    }
}