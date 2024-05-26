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
        //�߻� �ڵ�� SphereCast Ŭ�������� �����. ����-Ÿ�ټ��� ������

    }

    public void GunFire(Transform target) //���� �߻�
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

    //�̻��� �߻� �Լ�. �� Ŭ�������� �θ��°� �ƴ϶� SphereCast(���������� Ž������̶�� �����)Ŭ�������� �θ���
    public void LaunchMissile(Transform target)
    {
        Debug.Log(target);
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        Missile missileScript = missile.GetComponent<Missile>();
        missileScript.Launch(target, fighterController.Speed + 15, gameObject.layer);//���� ��ü �ӵ����� 15��ŭ �� ������ �߻�
    }
}