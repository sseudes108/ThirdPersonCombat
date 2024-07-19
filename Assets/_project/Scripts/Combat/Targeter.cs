using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour {
    private List<Target> _targets = new ();
    public Target CurrentTarget {get; private set;}
    private CinemachineTargetGroup _cinemachineTargetGroup;
    
    private void Awake() {
        _cinemachineTargetGroup = transform.parent.parent.Find("StateDrivenCamera").GetComponentInChildren<CinemachineTargetGroup>();
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.TryGetComponent(out Target target)){return;}
        
        _targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other) {
        if(!other.TryGetComponent(out Target target)){return;}
        RemoveTarget(target);
    }

    public bool SelectTarget(){
        if(_targets.Count == 0){return false;}
        CurrentTarget = _targets[0];
        _cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel(){
        if(CurrentTarget == null){return;}
        _cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target){
        if(CurrentTarget == target){
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        _targets.Remove(target);
    }
}