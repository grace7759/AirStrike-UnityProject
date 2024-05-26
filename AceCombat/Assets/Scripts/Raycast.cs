using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float lineSize = 10000f;

    void Update()
    {
        // Game화면에는 보이지않는다
        // Scene화면에는 보인다
        // 해당 위치부터, 특정 방향으로 길이만큼, yellow색상으로
        // 씬에서 눈으로 확인
        Debug.DrawRay(transform.position, transform.forward * lineSize, Color.yellow);

        //물체에 닿으면 true를 반환
        // 가까운 1개의 오브젝트만 감지한다.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lineSize))
        {
            // 닿은 물체의 이름을 출력
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("적군!!!!!!!!!!!!!!");
            }
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
