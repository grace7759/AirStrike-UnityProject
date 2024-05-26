using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 직선으로 충돌하는 객체가 있는 지 원 모양으로 RayCast를 한다.
/// </summary>
public class SphereCast : MonoBehaviour
{


    // ray의 길이
    [SerializeField]
    private float _maxDistance = 10000.0f;
    GameObject player;
    public GameObject aimBox;       //조준 박스는 사실 3D 텍스트 오브젝트임!! SphereCast를 오브젝트에 붙여넣으면서 조준 UI도 동시에 할 방법이 이거밖에 생각이 안나서..
    TextMesh aimBoxText;


    // ray의 색상
    [SerializeField]
    private Color _rayColor = Color.red;

    void OnDrawGizmos()
    {

       
        Gizmos.color = _rayColor;
        
        //float sphereScale = Mathf.Max(player.transform.lossyScale.x, player.transform.lossyScale.y, player.transform.lossyScale.z);

        // 함수 파라미터 : 현재 위치, Sphere의 크기(x,y,z 중 가장 큰 값이 크기가 됨), Ray의 방향, RaycastHit 결과, Sphere의 회전값, SphereCast를 진행할 거리
        if (Physics.SphereCast(transform.position, 5f , transform.forward, out RaycastHit hit, _maxDistance))
        {
            // Hit된 지점까지 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // Hit된 지점에 Sphere를 그려준다.
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, 5f);
            
            
        }
        else
        {
            // Hit가 되지 않았으면 최대 검출 거리로 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * _maxDistance);
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        aimBoxText = aimBox.GetComponent<TextMesh >();  
        aimBoxText.color = Color.green;
    }
    void Update()
    {
        //원형 레이저가 물체에 닿으면 그 물체 정보를 알려줌
        if (Physics.SphereCast(transform.position, 5f, transform.forward, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.gameObject.tag == "Enemy")//적군 기체면 조준 박스가 빨간색이 됨, 이 상태에서 미사일 발사 시 타겟 설정되어 Launch 실행
            {
                aimBoxText.color = Color.red;

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    WeaponController playerWeaponControllerScript = player.GetComponent<WeaponController>();
                    Debug.Log(hit.collider.gameObject.name);
                    playerWeaponControllerScript.LaunchMissile(hit.collider.gameObject.transform);
                }
                if (Input.GetKey(KeyCode.X))
                {
                    WeaponController playerWeaponControllerScript = player.GetComponent<WeaponController>();
                    Debug.Log("Gun Fire");
                    playerWeaponControllerScript.GunFire(hit.collider.gameObject.transform);
                }
            }
            else { 
                aimBoxText.color = Color.green;
            }
        }
        if(aimBoxText.color == Color.green)
        {
             //초록박스일 때 발사 누르면 그냥 일직선 발사
            if (Input.GetKeyDown(KeyCode.Z))
            {
                WeaponController playerWeaponControllerScript = player.GetComponent<WeaponController>();
                playerWeaponControllerScript.LaunchMissile(null);
            }
            if (Input.GetKey(KeyCode.X))
            {
                WeaponController playerWeaponControllerScript = player.GetComponent<WeaponController>();
                Debug.Log("Gun Fire");
                playerWeaponControllerScript.GunFire(null);
            }
        }
    }
}