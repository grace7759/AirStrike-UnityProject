using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FighterController : MonoBehaviour
{
    bool isStarted =false;
    bool isGameOver = false;
    bool isOnGround = true;
    bool isOnDest = false;
    public float rollAmount;
    public float pitchAmount;
    public float yawAmount;
    public float lerpAmount;
    public float speed;

    public float maxSpeed = 301.7f; // 최대 속력
    public float minSpeed = 10f;
    public float accelAmount; // 가속 
    public float brakeAmount;

    float speedReciprocal; // maxSpeed의 역수
    float accelValue;
    float brakeValue;

    public RawImage planeImg;
    public GameObject explosionPrefab;
    public GameObject smokePrefab;

    Vector3 rotateValue;
    Rigidbody playerRigidbody;
    public Vector3 destinationPoint;
    Vector3 startPoint;
    float destDistance;
    float startDistance;
    public int hp = 100;

    void FighterRotation()
    {
        //인풋따라 회전
        float rollInput = Input.GetAxis("Roll");
        float pitchInput = Input.GetAxis("Pitch");
        float yawInput = Input.GetAxis("Yaw");
        Vector3 lerpVector = new Vector3(pitchInput * pitchAmount, yawInput * yawAmount, -rollInput * rollAmount);
        rotateValue = Vector3.Lerp(rotateValue, lerpVector, lerpAmount * Time.fixedDeltaTime);

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(rotateValue * Time.fixedDeltaTime));

        //스피드 조정
        speedReciprocal = 1 / maxSpeed;
        
        float accelEase = (maxSpeed - speed) * speedReciprocal;
        if (Input.GetKey(KeyCode.Space))
        {
            speed += accelAmount * Time.fixedDeltaTime*accelEase;
        }
        float brakeEase = (speed - minSpeed) * speedReciprocal;
        if (!Input.GetKey(KeyCode.Space))
        {
            speed -= brakeAmount * 30 * Time.fixedDeltaTime * brakeEase;
        }
        
        playerRigidbody.velocity = transform.forward * speed;


    }
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        destinationPoint = new Vector3(100, 0, 700);    //출발 도착지를 여기서 정해놧는데 출발도착지를 정의하는 새로운 스크립트 생성해서 구현하는게 나아보임.
        startPoint = transform.position;
        
        speed = 0;
        isGameOver = false;
    }

    void FixedUpdate()
    {
        destDistance = Vector3.Distance(destinationPoint, transform.position);
        startDistance= Vector3.Distance(startPoint, transform.position);
        if (isStarted&&!isGameOver)
        {
            FighterRotation();
            planeImg.color = Color.green;
        }
        
        if (isOnGround&&Input.GetKeyUp(KeyCode.G))
        {
            isStarted = true;
            
            if (isOnDest)
            {
                
                isOnDest = false;
                Debug.Log(destinationPoint);
            }
            isOnGround = false;
        }
        if (!isOnGround&&startDistance <= 100f)   //출발시 이륙
        {
            Vector3 newpos=transform.position + Vector3.up*4;
            //playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(-transform.forward));
            speed = Mathf.Lerp(speed, 100, startDistance*0.001f);
            transform.position = Vector3.Lerp(transform.position, newpos, startDistance*0.002f);
        }
        if (speed <= 50 && destDistance <= 40f) //목적지 도착시 착륙
        {
            speed = Mathf.Lerp(speed, 0, destDistance / 20f);
            transform.position = Vector3.Lerp(transform.position, destinationPoint, destDistance * 0.002f);
            Debug.Log(destDistance);
            
        }
        if (destDistance<6f)
        {
            isOnGround = true;
            isOnDest = true;
            Debug.Log("on Dest");
            //transform.LookAt(destinationPoint);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 53f, transform.rotation.z), Time.deltaTime);
        }

        if (hp<=0&&!isGameOver)
        {
            speed = 0;
            Die();
        }
        if (hp <= 0 && isGameOver)
        {
            Instantiate(smokePrefab, transform.position, Quaternion.identity);
        }

    }

    void OnCollisionEnter(Collision other)
    {
        //충돌감지. 나중에 출발도착지 관련 제한사항을 조건문에 추가해야할지도
        if (!(other.collider.gameObject.CompareTag("playerBullet"))) {
            Debug.Log("충돌");
            hp = (int)((double)hp - speed * 0.1);
            planeImg.color = Color.red; //기체 UI에 반영
        }



    }

    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        playerRigidbody.useGravity = true;  //중력 사용해서 떨어지는 모션
        playerRigidbody.freezeRotation = false; //이거 안하면 너무 뱅글뱅글 돌게됨
        GameObject gameManager = GameObject.Find("GameManager");
        GameManager gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.EndGame();//게임 끝
        isStarted = false;
        isGameOver = true;
        
    }
 
    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public string Points(string option)//목적지 출발지 구하기 위한 함수 그냥 시간낭비...스크립트 새로 짜야함 그냥
    {
        if (option == "dest")
        {
            return ((string)("("+destinationPoint.x+", "+destinationPoint.y+", "+destinationPoint.z+")"));
        }
        else if (option == "start")
        {
            return ((string)"("+startPoint.x+", "+startPoint.y+", "+startPoint.z+")");
        }
        else
        {
            return "no";
        }
    }
}
