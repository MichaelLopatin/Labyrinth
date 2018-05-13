using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour 
{
    public delegate void DemageFromBullet(WeaponType weapon);
    public event DemageFromBullet DemageFromBulletEvent;

    public enum WeaponType
    {
        pistol,
        machineGun,
        shotgun,
        rocketLauncher
    }

    private WeaponType curentWeapon;
    private WeaponType lastWeapon;

    private void Start()
    {
        curentWeapon = lastWeapon = WeaponType.pistol;
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
            if (DemageFromBulletEvent != null)
            {
                DemageFromBulletEvent(curentWeapon);
            }
        }
    }
}
