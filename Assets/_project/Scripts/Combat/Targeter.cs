using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour {
    private List<Target> _targets = new ();
    public Target CurrentTarget {get; private set;}
    public CinemachineTargetGroup CinemachineTargetGroup;
    private Camera _mainCamera;

    Target _targetT;
    
    private void Awake() {
        CinemachineTargetGroup = transform.parent.parent.Find("StateDrivenCamera").GetComponentInChildren<CinemachineTargetGroup>();
        _mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.TryGetComponent(out Target target)){return;}
        
        _targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other) {
        if(!other.TryGetComponent(out Target target)){return;}
        RemoveTarget(target);
        _targetT = target;
    }

    public bool SelectTarget(){
        if(_targets.Count == 0){return false;}

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach(var target in _targets){
            Vector2 viewPos = _mainCamera.WorldToViewportPoint(target.transform.position);

            if(!target.transform.Find("Model").GetComponentInChildren<Renderer>().isVisible){
                Debug.Log($"isVisible: {target.transform.Find("Model").GetComponentInChildren<Renderer>().isVisible}");
                continue;
            }
            
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude < closestTargetDistance){
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }
        
        if(closestTarget == null){return false;}

        CurrentTarget = closestTarget;
        CinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel(){
        RemoveT();
        if(CurrentTarget == null){return;}
        CinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target){
        if(CurrentTarget == target){
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        _targets.Remove(target);
    }

    public void RemoveT(){
        if(_targetT == null) { return; }
        CinemachineTargetGroup.RemoveMember(_targetT.transform);
        _targetT = null;
    }
}