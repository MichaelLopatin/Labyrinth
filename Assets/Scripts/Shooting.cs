using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public delegate void GetDamage(int damage);
    public static event GetDamage GetDamageEvent;

    public WeaponType curentWeapon;
    private WeaponType delayChangeWeapon;
    public bool isShooting = false;

    [SerializeField] private Camera playerCamera;
    private Transform playerCameraTransform;

    private float pistolReloadTime = 0.5f;
    private float machineGunReloadTime = 0.2f;
    private float shotgunReloadTime = 1f;
    private float rocketReloadTime = 3f;

   private Weapon pistol;
    private Weapon machineGun;

    private IEnumerator fireCoroutine;

    private enum Damage
    {
        pistol = 20,
        machineGun = 20,
        shotgunPellet = 5,
        rocket = 80,
        rocketSplinter = 50
    }

    private struct Weapon
    {
      public  float reloadTime;
        int damage;
        public Weapon(float reloadTime, int damage)
        {
            this.reloadTime = reloadTime;
            this.damage = damage;
        }
    }


    private void Awake()
    {
        curentWeapon = delayChangeWeapon = WeaponType.pistol;
        playerCameraTransform = playerCamera.transform;
        pistol = new Weapon(0.5f, 15);
        machineGun = new Weapon(0.2f, 15);
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
        if (!isShooting)
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
                    fireCoroutine = PistolShootingCoroutine(pistol.reloadTime);
                    break;
                case WeaponType.machineGun:
                    fireCoroutine = MachineGunShootingCoroutine(machineGunReloadTime);
                    break;
                case WeaponType.shotgun:
                    fireCoroutine = ShotgunShootingCoroutine(shotgunReloadTime);
                    break;
                case WeaponType.rocketLauncher:
                    fireCoroutine = RocketLauncherShootingCoroutine(rocketReloadTime);
                    break;
                default:
                    break;
            }
            StartCoroutine(fireCoroutine);
            isShooting = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
            isShooting = false;
            if (curentWeapon != delayChangeWeapon)
            {
                curentWeapon = delayChangeWeapon;
            }
        }

    }

    private IEnumerator PistolShootingCoroutine(float waitTime)
    {
        yield return null;
        Vector3 shotDirection;
        RaycastHit rayHit;

        while (true)
        {
            shotDirection = playerCameraTransform.TransformDirection(Vector3.forward);
            //  Debug.DrawRay(playerCameraTransform.position, shotDirection);
            if (Physics.Raycast(playerCameraTransform.position, shotDirection, out rayHit, 100))
            {
                if (rayHit.collider.CompareTag("TagEnemy"))
                {
                    if (GetDamageEvent != null)
                    {
                        GetDamageEvent(1);
                    }
                }
                //   print(rayHit.collider.name);
            }
            yield return new WaitForSeconds(waitTime);
        }


    }
    private IEnumerator MachineGunShootingCoroutine(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }
    private IEnumerator ShotgunShootingCoroutine(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }
    private IEnumerator RocketLauncherShootingCoroutine(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }
}