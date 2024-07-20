using UnityEngine;

public class ForceReceiver : MonoBehaviour {
    private CharacterController Controller;
    private float _verticalVelocity;
    [SerializeField] private float _drag = 0.3f;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Awake() {
        Controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if(Controller.isGrounded && _verticalVelocity <= 0f){
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }else{
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }

    public void AddForce(Vector3 force){
        _impact += force;
    }
}