using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "3dPersonCombat/Attack")]
public class AttackSO : ScriptableObject {
    [field:SerializeField] public string AnimationName { get; private set; }
    [field:SerializeField] public float TransitionDuration { get; private set; }
    
    [field:SerializeField] public float ComboAttackTime { get; private set; }

    [field:SerializeField] public float Force { get; private set; } //Force to move the attacker front 
    [field:SerializeField] public float ForceTime { get; private set; }

    [field:SerializeField] public int AttackDamage { get; private set; }
    [field:SerializeField] public int KnockbackForce { get; private set; }
}