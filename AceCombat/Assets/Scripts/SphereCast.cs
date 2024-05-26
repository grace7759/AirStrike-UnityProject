using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� �浹�ϴ� ��ü�� �ִ� �� �� ������� RayCast�� �Ѵ�.
/// </summary>
public class SphereCast : MonoBehaviour
{


    // ray�� ����
    [SerializeField]
    private float _maxDistance = 10000.0f;
    GameObject player;
    public GameObject aimBox;       //���� �ڽ��� ��� 3D �ؽ�Ʈ ������Ʈ��!! SphereCast�� ������Ʈ�� �ٿ������鼭 ���� UI�� ���ÿ� �� ����� �̰Źۿ� ������ �ȳ���..
    TextMesh aimBoxText;


    // ray�� ����
    [SerializeField]
    private Color _rayColor = Color.red;

    void OnDrawGizmos()
    {

       
        Gizmos.color = _rayColor;
        
        //float sphereScale = Mathf.Max(player.transform.lossyScale.x, player.transform.lossyScale.y, player.transform.lossyScale.z);

        // �Լ� �Ķ���� : ���� ��ġ, Sphere�� ũ��(x,y,z �� ���� ū ���� ũ�Ⱑ ��), Ray�� ����, RaycastHit ���, Sphere�� ȸ����, SphereCast�� ������ �Ÿ�
        if (Physics.SphereCast(transform.position, 5f , transform.forward, out RaycastHit hit, _maxDistance))
        {
            // Hit�� �������� ray�� �׷��ش�.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // Hit�� ������ Sphere�� �׷��ش�.
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, 5f);
            
            
        }
        else
        {
            // Hit�� ���� �ʾ����� �ִ� ���� �Ÿ��� ray�� �׷��ش�.
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
        //���� �������� ��ü�� ������ �� ��ü ������ �˷���
        if (Physics.SphereCast(transform.position, 5f, transform.forward, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.gameObject.tag == "Enemy")//���� ��ü�� ���� �ڽ��� �������� ��, �� ���¿��� �̻��� �߻� �� Ÿ�� �����Ǿ� Launch ����
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
             //�ʷϹڽ��� �� �߻� ������ �׳� ������ �߻�
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