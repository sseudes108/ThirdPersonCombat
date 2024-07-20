using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour {
    [SerializeField] private Collider _playerCollider;
    private List<Collider> _alreadyCollidedWith = new(); //Prevent multiple damages in same hit
    private int _damage;

    private void OnEnable() {
        _alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if(other == _playerCollider){return;}
        if(_alreadyCollidedWith.Contains(other)){return;}
        
        _alreadyCollidedWith.Add(other);

        other.TryGetComponent(out Health health);
        if(health != null){
            health.DealDamage(_damage);
        }
    }

    public void SetDamage(int amount) {
        _damage = amount;
    }
}