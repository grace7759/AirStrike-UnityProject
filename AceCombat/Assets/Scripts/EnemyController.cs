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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turningForce * Time.deltaTime);//�̻��� ���� �ڵ�� �Ȱ���

        speed = player.GetComponent<FighterController>().speed * 0.7f;  //�ٵ� �ӵ��� �� �ӵ��� 0.7...�̰Ŵ� �����غ��� ���� �ȵ�� �ٲٵ����ص���
        enemyRigidBody.velocity = transform.forward * speed;    

        if (hp <= 0)
        {
            
            gameObject.SetActive(false);    //ü���� 0�̵Ǹ� ���ش�. ������ ���� �굵 �߷¿�������鼭 �߶������� ���ڴ�!�� �������ּ���
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        //�浹������ ����, �̷�
        if (other.gameObject.CompareTag("playerBullet"))
        {
            Debug.Log("�Ѿ��浹");
            hp = hp - 10;
        }
        if (other.gameObject.CompareTag("playerMissile"))
        {
            Debug.Log("�̻����浹");
            hp = hp - 100 ;
        }

    }
}
