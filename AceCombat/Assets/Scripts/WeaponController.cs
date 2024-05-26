using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FighterController))]

public class WeaponController : MonoBehaviour
{
    // Weapon Inputs
    bool useSpecialWeapon;
    bool isGunFiring;
    public GameObject missilePrefab;
    public GameObject gunPrefab;
    public GameObject explosionPrefab;
    FighterController fighterController;

    // Weapon Callbacks
    public void Update()
    {
        //발사 코드는 SphereCast 클래스에서 실행됨. 조준-타겟설정 때문에

    }

    public void GunFire(Transform target) //기총 발사
    {
        GameObject gun = Instantiate(gunPrefab, transform.position, transform.rotation);
        if (target)
        {
            gun.transform.LookAt(target);
        }
        Gun gunScript = gun.GetComponent<Gun>();
        gunScript.Fire(fighterController.Speed + 70);
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
    void Start()
    {
        fighterController = gameObject.GetComponent<FighterController>();
    }

    //미사일 발사 함수. 이 클래스에서 부르는게 아니라 SphereCast(적군레이저 탐지기능이라고 보면됨)클래스에서 부른다
    public void LaunchMissile(Transform target)
    {
        Debug.Log(target);
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        Missile missileScript = missile.GetComponent<Missile>();
        missileScript.Launch(target, fighterController.Speed + 15, gameObject.layer);//지금 기체 속도에서 15만큼 더 빠르게 발사
    }
}