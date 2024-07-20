public class PlayerAttackingState : PlayerBaseState{
    public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine){}
    public AttackSO Attack { get; private set; }
    private bool _alreadyAppliedForce;

    public override void Enter(){
        _alreadyAppliedForce = false;
        Attack = GetCurrentAttack();
        StateMachine.WeaponDamage.SetDamage(Attack.AttackDamage);
        StateMachine.Animator.CrossFadeInFixedTime(Attack.AnimationName, Attack.TransitionDuration);
    }

    public override void Tick(float deltaTime){
        Move(deltaTime);
        FaceTarget(deltaTime);
        float normalizedTime = GetNormalizedTime();

        if(normalizedTime < 1f){
            if(normalizedTime > Attack.ForceTime){
                TryApplyForce();
            }

            if(StateMachine.InputReader.IsAttacking){
                TryComboAttack(normalizedTime);
            }
        }else{
            if(StateMachine.Targeter.CurrentTarget != null){
                StateMachine.SwitchState(StateMachine.PlayerStates.Targeting);
            }else{
                StateMachine.SwitchState(StateMachine.PlayerStates.FreeLook);
            }
            ResetAttackIndex();
        }
    }


    public override void Exit(){

    }

    public override string ToString(){
        return "Attack";
    }

    private void TryComboAttack(float normalizedTime){
        if(Attack.ComboStateIndex == StateMachine.Attacks.Count -1){return;}
        if(normalizedTime < Attack.ComboAttackTime){return;}

        ChainAttack();
        StateMachine.SwitchState(StateMachine.PlayerStates.Attacking);
    }

    private void TryApplyForce(){
        if(_alreadyAppliedForce){return;}

        StateMachine.ForceReceiver.AddForce(StateMachine.transform.forward * Attack.Force);
        _alreadyAppliedForce = true;
    }

    private float GetNormalizedTime(){
        var currentInfo = StateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        var nextInfo = StateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(StateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack")){
            return nextInfo.normalizedTime;
        }else if(!StateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack")){
            return currentInfo.normalizedTime;
        }else{
            return 0;
        }
    }
}
