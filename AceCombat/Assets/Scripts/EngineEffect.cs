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
        if (Input.GetKey(KeyCode.Space))    //스페이스바 눌러 가속하면 효과생김
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
