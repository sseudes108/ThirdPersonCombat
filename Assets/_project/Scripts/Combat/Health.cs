using System;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private bool _isDead;

    private void Start() {
        _isDead = false;
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damageAmount){
        if(_isDead) { return; }
        _currentHealth = Mathf.Max(_currentHealth - damageAmount, 0);
    }

    private void Die(){
        _isDead = true;
        Destroy(gameObject);
    }
}