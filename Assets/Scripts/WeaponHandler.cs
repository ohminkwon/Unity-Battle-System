using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;

    //Both functions for entering or exiting certain animation event points
    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }
    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }
}
