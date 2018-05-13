using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private WeaponType curentWeapon;
    private WeaponType delayChangeWeapon;
    private bool isShooting = false;

    private enum Damage
    {
        pistol=20,
        machineGun=20,
        shotgunPellet=5,
        rocket=80,
        rocketSplinter=50
    }
    private void Awake()
    {
        curentWeapon = delayChangeWeapon = WeaponType.pistol;
    }

    private void OnEnable()
    {
        ChangeWeapon.CurentWeaponEvent += CurentWeapon;
    }

    private void OnDisable()
    {
        ChangeWeapon.CurentWeaponEvent -= CurentWeapon;
    }


    private void CurentWeapon(WeaponType weapon)
    {
        if(!isShooting)
        {
        curentWeapon = weapon;
            delayChangeWeapon = weapon;
        }
        else
        {
            delayChangeWeapon = weapon;
        }

    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            switch (curentWeapon)
            {
                case WeaponType.pistol:
                    StartCoroutine(PistolShootingCoroutine());
                    break;
                case WeaponType.machineGun:
                    StartCoroutine(MachineGunShootingCoroutine());
                    break;
                case WeaponType.shotgun:
                    StartCoroutine(ShotgunShootingCoroutine());
                    break;
                case WeaponType.rocketLauncher:
                    StartCoroutine(RocketLauncherShootingCoroutine());
                    break;
                default:
                    break;
            }
            isShooting = true;
        }
        if (Input.GetButtonUp("Fire1") && isShooting)
        {
            switch(curentWeapon)
            {
                case WeaponType.pistol:
                    StopCoroutine(PistolShootingCoroutine());
                    break;
                case WeaponType.machineGun:
                    StopCoroutine(MachineGunShootingCoroutine());
                    break;
                case WeaponType.shotgun:
                    StopCoroutine(ShotgunShootingCoroutine());
                    break;
                case WeaponType.rocketLauncher:
                    StopCoroutine(RocketLauncherShootingCoroutine());
                    break;
                default:
                    break;
            }
            isShooting = false;
        }
    }

    private IEnumerator PistolShootingCoroutine()
    {

        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator MachineGunShootingCoroutine()
    {

        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator ShotgunShootingCoroutine()
    {

        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator RocketLauncherShootingCoroutine()
    {

        yield return new WaitForSeconds(0.5f);
    }
}