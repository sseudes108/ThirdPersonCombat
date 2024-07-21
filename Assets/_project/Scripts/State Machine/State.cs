using UnityEngine;

public abstract class State {
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    protected void Move(StateMachine stateMachine, Vector3 motion, float deltaTime){
        stateMachine.Controller.Move((motion +  stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(StateMachine stateMachine, float deltaTime){
        Move(stateMachine, Vector3.zero, deltaTime);
    }

    protected float GetNormalizedTime(Animator animator){
        var currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        var nextInfo = animator.GetNextAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag("Attack")){
            return nextInfo.normalizedTime;
        }else if(!animator.IsInTransition(0) && currentInfo.IsTag("Attack")){
            return currentInfo.normalizedTime;
        }else{
            return 0;
        }
    }
}