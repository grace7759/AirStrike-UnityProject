using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float maxSpeed;
    public float accelAmount;
    public float lifetime;
    public GameObject explosionPrefab;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public void Fire(float launchSpeed)
    {
        speed = launchSpeed;
        Rigidbody bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            speed += accelAmount * Time.deltaTime;
        }

        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }
    void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("player")))   //�ٵ� �̰� ����� �۵����ϴ°Ͱ���...�ݸ��� ���̾�� �۵��ϴ°Ͱ���.
        {
            Explode();
            Destroy(gameObject);
        }

    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
