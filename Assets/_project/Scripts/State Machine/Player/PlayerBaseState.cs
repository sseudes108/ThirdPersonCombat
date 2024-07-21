using UnityEngine;

public abstract class PlayerBaseState : State {
    // protected Vector3 _dodgeDirection;
    public PlayerBaseState(PlayerStateMachine stateMachine){
        StateMachine = stateMachine;
    }

    protected int _attackIndex = 0;
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

    protected void ReturnToLocomotion(){
        if(StateMachine.Targeter.CurrentTarget != null){
            StateMachine.SwitchState(StateMachine.PlayerStates.Targeting);
            return;
        }
        StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
    }
}