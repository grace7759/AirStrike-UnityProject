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

    
    //미사일이 타겟 따라가는 함수
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

        //transform.LookAt(target); //--> 너무 급격하게 꺾어서 안좋다.
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turningForce * Time.deltaTime);
    }

    //player가 가지고 있는 WeaponController에서 이 함수를 불러서 발사함
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
        Destroy(gameObject, lifetime);  //생존시간
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
            Debug.Log("플레이어아님");
        }
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
