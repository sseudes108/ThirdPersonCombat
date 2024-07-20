using UnityEngine;

public class WeaponHandler : MonoBehaviour{
    [SerializeField] private GameObject _weaponLogic;

    public void EnableHitBox(){
        _weaponLogic.SetActive(true);
    }
    
    public void DisableHitBox(){
        _weaponLogic.SetActive(false);
    }
}
