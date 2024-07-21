using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour {
    [SerializeField] private Collider _myCollider;
    private List<Collider> _alreadyCollidedWith = new(); //Prevent multiple damages in same hit
    private int _damage;
    private float _knockbackForce;

    private void OnDisable() {
        _alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if(other == _myCollider){return;}
        if(_alreadyCollidedWith.Contains(other)){return;}
    
        _alreadyCollidedWith.Add(other);

        other.TryGetComponent(out Health health);
        if(health != null){
            health.DealDamage(_damage);
        }

        other.TryGetComponent(out ForceReceiver forceReceiver);
        if(forceReceiver != null){
            Vector3 direction = (other.transform.position - _myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * _knockbackForce);
        }
    }

    public void SetDamage(int amount, float knockbackForce) {
        _damage = amount;
        _knockbackForce = knockbackForce;
    }
}