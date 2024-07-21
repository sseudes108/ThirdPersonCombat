using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerStateMachine : StateMachine {
    [field:SerializeField] public PlayerStatesSO PlayerStates { get; private set; }

    public InputReader InputReader { get; private set; }
    public Targeter Targeter { get; private set; }
    public Transform MainCameraPosition { get; private set; }
 
    [field:SerializeField] public float FreelookMovementSpeed { get; private set; }
    [field:SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field:SerializeField] public float RotationSmoothValue { get; private set; }

    [field:SerializeField] public float DodgeDuration { get; private set; }
    [field:SerializeField] public float DodgeLenght { get; private set; }

    [field:SerializeField] public float JumpForce { get; private set; }

    private void Start() {
        SwitchState(PlayerStates.FreeLook);
    }

    public override void Update() {
        base.Update();
        if(InputReader.IsBlocking){
             
            SwitchState(PlayerStates.Blocking);
        }
    }
    
    public override void SwitchState(State newState){
        base.SwitchState(newState);
        Tester.Instance.UpdateStateLabel(newState.ToString());
    }

    public override void SetStates(){
        PlayerStates.SetStateMachine(this);
        PlayerStates.CreateStates();
    }

    public override void SetComponents(){
        base.SetComponents();
        InputReader = GetComponent<InputReader>();
        Targeter = GetComponentInChildren<Targeter>();
        MainCameraPosition = Camera.main.transform;
    }

    public override void OnDamageTaken(){
        SwitchState(PlayerStates.Impact);
    }

    public override void OnDie(){
        InputReader.ResetActions();
        SwitchState(PlayerStates.Dead);
    }
}