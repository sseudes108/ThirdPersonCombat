using UnityEngine;

public abstract class PlayerBaseState : State {
    public PlayerBaseState(PlayerStateMachine stateMachine){
        StateMachine = stateMachine;
    }

    private int _attackIndex = 0;
    public PlayerStateMachine StateMachine { get; private set; }

    public abstract override string ToString();

    protected void ChainAttack(){
        _attackIndex++;
        if(_attackIndex > StateMachine.Attacks.Count){
            _attackIndex = 0;
        }
    }

    protected AttackSO GetCurrentAttack(){
        return StateMachine.Attacks[_attackIndex];
    }

    protected void ResetAttackIndex(){
        _attackIndex = 0;
    }

    protected void Move(Vector3 motion, float deltaTime){
        StateMachine.Controller.Move((motion +  StateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime){
        Move(Vector3.zero, deltaTime);
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