using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Text destDistanceText;
    GameObject pressGtoStartText;   //맨처음 뜨는 G버튼 누르시오 텍스트
    public Text destinationPointText;   //목적지 텍스크
    public Text hpText; //체력 텍스트
    public Text startPointText;//출발지 좌표 텍스트
    GameObject player;  //내 기체 오브젝트
    FighterController playerComponent;  //기체 오브젝트의 컨트롤러 스크립트
    //Vector3 startPoint;
    public GameObject restart;//게임 끝나고 나오는 재시작 버튼 오브젝트
    bool isGameOver;    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<FighterController>();
        pressGtoStartText = GameObject.Find("PressGtoStart");
        restart.SetActive(false);
        isGameOver = false;
        hpText.text = "HP: " + playerComponent.hp;


    }

    // Update is called once per frame
    void Update()
    {

        destinationPointText.text = "Destination: "+playerComponent.Points("dest");
        startPointText.text = "Starting Point: "+playerComponent.Points("start");
        //destinationPointText.text = "distance to dest: " + (int)destination;
        hpText.text = "HP: " + playerComponent.hp;

        if (Input.GetKeyDown(KeyCode.G))
        {
            pressGtoStartText.SetActive(false);
        }
    }


    public void EndGame()
    {
        isGameOver = true;
        restart.SetActive(true);
    }
   
}
