using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp;
    private Rigidbody enemyRigidBody;
    public float speed;
    GameObject player;
    public float turningForce;
    public Transform target;
    void Start()
    {
        hp = 100;
        this.transform.LookAt(target);
        enemyRigidBody = GetComponent<Rigidbody>();
        target = FindObjectOfType<FighterController>().transform;
        player = GameObject.Find("Player");
        
    }
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turningForce * Time.deltaTime);//미사일 유도 코드랑 똑같음

        speed = player.GetComponent<FighterController>().speed * 0.7f;  //근데 속도가 내 속도의 0.7...이거는 생각해보고 맘에 안들면 바꾸든지해도됨
        enemyRigidBody.velocity = transform.forward * speed;    

        if (hp <= 0)
        {
            
            gameObject.SetActive(false);    //체력이 0이되면 없앤다. 없애지 말고 얘도 중력영향받으면서 추락했으면 좋겠다!면 구현해주세요
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        //충돌감지와 착륙, 이륙
        if (other.gameObject.CompareTag("playerBullet"))
        {
            Debug.Log("총알충돌");
            hp = hp - 10;
        }
        if (other.gameObject.CompareTag("playerMissile"))
        {
            Debug.Log("미사일충돌");
            hp = hp - 100 ;
        }

    }
}
