using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputReader), typeof(ForceReceiver))]
public class PlayerStateMachine : StateMachine {
    [field:SerializeField] public PlayerStatesSO PlayerStates { get; private set; }

    public InputReader InputReader { get; private set; }
    public CharacterController Controller { get; private set; }
    public Animator Animator { get; private set; }
    public Targeter Targeter { get; private set; }
    public Transform MainCameraPosition { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    [field:SerializeField] public WeaponDamage WeaponDamage { get; private set; }

    [field:SerializeField] public List<AttackSO> Attacks { get; private set; }
 
    [field:SerializeField] public float FreelookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field:SerializeField] public float RotationSmoothValue { get; private set; }

    private void Awake() {
        SetComponents();
        SetStates();
    }

    private void Start() {
        SwitchState(PlayerStates.FreeLook);
    }
    
    public override void SwitchState(State newState){
        base.SwitchState(newState);
        Tester.Instance.UpdateStateLabel(newState.ToString());
    }

    private void SetStates(){
        PlayerStates.SetStateMachine(this);
        PlayerStates.CreateStates();
    }

    private void SetComponents(){
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
        Targeter = GetComponentInChildren<Targeter>();
        ForceReceiver = GetComponent<ForceReceiver>();
        MainCameraPosition = Camera.main.transform;
    }

}