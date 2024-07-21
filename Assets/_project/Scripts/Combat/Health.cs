using System;
using UnityEngine;

public class Health : MonoBehaviour {
    public event Action OnDamageTaken;
    public event Action OnDie;
    public bool IsDead { get; private set; }
    private StateMachine _stateMachine;

    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private bool _invulnerable;
    private void Awake() {
        _stateMachine = GetComponent<StateMachine>();
    }

    private void Start() {
        IsDead = false;
        _invulnerable = false;
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damageAmount){
        if(_currentHealth == 0) { return; }
        if(_invulnerable) { return; }

        if(_stateMachine is PlayerStateMachine && _stateMachine.CurrentState == (_stateMachine as PlayerStateMachine).PlayerStates.Blocking){
            damageAmount = (int)Mathf.Round(damageAmount /= 3);
        }
        
        _currentHealth = Mathf.Max(_currentHealth - damageAmount, 0);
        OnDamageTaken?.Invoke();
        if(_currentHealth <= 0){
            _currentHealth = 0;
            Die();
        }
    }

    private void Die(){
        IsDead = true;
        OnDie?.Invoke();
    }

    public void SetInvulnerable(bool value){
        _invulnerable = value;
    }
}