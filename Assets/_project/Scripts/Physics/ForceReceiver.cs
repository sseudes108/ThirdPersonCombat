using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour {
    private CharacterController _controller;
    private NavMeshAgent _navMeshAgent;
    private float _verticalVelocity;
    [SerializeField] private float _drag = 0.3f;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Awake() {
        _controller = GetComponent<CharacterController>();
        TryGetComponent(out _navMeshAgent);
    }

    private void Update() {
        if(_controller.isGrounded && _verticalVelocity <= 0f){
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }else{
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);

        if(_impact.sqrMagnitude <= 0.2f * 0.2f && _navMeshAgent != null){
            _impact = Vector3.zero;
            _navMeshAgent.enabled = true;
        }
    }

    public void AddForce(Vector3 force){
        _impact += force;
        if(_navMeshAgent != null){
            _navMeshAgent.enabled = false;
        }
    }

    public void Jump(float jumpForce){
        _verticalVelocity += jumpForce;
    }
}