using TMPro;
using UnityEngine;

public class Tester : MonoBehaviour{
    public static Tester Instance { get; private set;}
    [SerializeField] private TesterLibSO Lib;
    [SerializeField] private TextMeshProUGUI _fpsLabel;
    [SerializeField] private TextMeshProUGUI _stateLabel;
    private float deltaTime = 0.0f;
    
    private void Awake() {
        if(Instance != null) {
            Debug.LogError("More than one instance of Tester.cs");
            Destroy(Instance);
        }
        Instance = this;
    }

    public void UpdateStateLabel(string state){
        _stateLabel.text = $"State: {state}";
    }


    private void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fpsLabel.text = $"Fps: {Mathf.Ceil(fps)}";
    }
}