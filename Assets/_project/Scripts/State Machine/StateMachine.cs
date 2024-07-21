using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(ForceReceiver))]
public abstract class StateMachine : MonoBehaviour {
    public State CurrentState { get; private set; }

    public CharacterController Controller { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public Health Health { get; private set; }

    [field:SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field:SerializeField] public List<AttackSO> Attacks { get; private set; }

    public virtual void OnEnable() {
        Health.OnDamageTaken += OnDamageTaken;
        Health.OnDie += OnDie;
    }

    public virtual void OnDisable() {
        Health.OnDamageTaken -= OnDamageTaken;
        Health.OnDie -= OnDie;
    }

    public virtual void Awake() {
        SetStates();
        SetComponents();
    }

    public virtual void Update() {
        CurrentState.Tick(Time.deltaTime);
    }

    public virtual void SwitchState(State newState){
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public virtual void SetStates(){}

    public virtual void SetComponents(){
        Controller = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Health = GetComponent<Health>();
    }

    public abstract void OnDamageTaken();
    public abstract void OnDie();
}