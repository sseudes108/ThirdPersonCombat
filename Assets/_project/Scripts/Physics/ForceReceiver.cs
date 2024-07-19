using UnityEngine;

public class ForceReceiver : MonoBehaviour {
    private CharacterController Controller;
    private float _verticalVelocity;
    public Vector3 Movement => Vector3.up * _verticalVelocity;

    private void Awake() {
        Controller = GetComponent<CharacterController>();
    }

    private void Update() {
        if(Controller.isGrounded && _verticalVelocity <= 0f){
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }else{
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}