using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineEffect : MonoBehaviour
{

    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))    //�����̽��� ���� �����ϸ� ȿ������
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
