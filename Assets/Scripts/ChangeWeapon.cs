using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum WeaponType
    {
        pistol,
        machineGun,
        shotgun,
        rocketLauncher
    }


public class ChangeWeapon : MonoBehaviour 
{
    public delegate void CurentWeapon(WeaponType weapon);
    public static event CurentWeapon CurentWeaponEvent;



    private WeaponType curentWeapon;
    private WeaponType lastWeapon;

    private void Start()
    {
        curentWeapon = lastWeapon = WeaponType.pistol;
        if (CurentWeaponEvent != null)
        {
            CurentWeaponEvent(curentWeapon);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curentWeapon = WeaponType.pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curentWeapon = WeaponType.machineGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            curentWeapon = WeaponType.shotgun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            curentWeapon = WeaponType.rocketLauncher;
        }
        if (lastWeapon != curentWeapon)
        {
            lastWeapon = curentWeapon;
            if (CurentWeaponEvent != null)
            {
                CurentWeaponEvent(curentWeapon);
            }
        }
    }
}
