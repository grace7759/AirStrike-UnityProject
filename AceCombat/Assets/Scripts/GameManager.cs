using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Text destDistanceText;
    GameObject pressGtoStartText;   //��ó�� �ߴ� G��ư �����ÿ� �ؽ�Ʈ
    public Text destinationPointText;   //������ �ؽ�ũ
    public Text hpText; //ü�� �ؽ�Ʈ
    public Text startPointText;//����� ��ǥ �ؽ�Ʈ
    GameObject player;  //�� ��ü ������Ʈ
    FighterController playerComponent;  //��ü ������Ʈ�� ��Ʈ�ѷ� ��ũ��Ʈ
    //Vector3 startPoint;
    public GameObject restart;//���� ������ ������ ����� ��ư ������Ʈ
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
