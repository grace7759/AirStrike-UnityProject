using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Transform target;
    public float turningForce;

    public float maxSpeed;
    public float accelAmount;
    public float lifetime;
    float speed;
    public float boresightAngle;
    public GameObject explosionPrefab;

    
    //�̻����� Ÿ�� ���󰡴� �Լ�
    void LookAtTarget()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        /*if (angle > boresightAngle)
        {
            target = null;
            return;
        }*/

        //transform.LookAt(target); //--> �ʹ� �ް��ϰ� ��� ������.
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turningForce * Time.deltaTime);
    }

    //player�� ������ �ִ� WeaponController���� �� �Լ��� �ҷ��� �߻���
    public void Launch(Transform target, float launchSpeed,int layer)
    {
        if (target)
        {
            this.target = target;
        }
        
        speed = launchSpeed;
        gameObject.layer = layer;
        Rigidbody missileRigidbody = GetComponent<Rigidbody>();
        missileRigidbody.velocity = transform.forward * speed;
    }
   
    void Start()
    {
        Destroy(gameObject, lifetime);  //�����ð�
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            speed += accelAmount * Time.deltaTime;
        }

        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        LookAtTarget();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("player")))
        {
            Explode();
            Destroy(gameObject);
            Debug.Log("�÷��̾�ƴ�");
        }
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
