using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1f;
    private Vector3 velocity;

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // ��� ������ ������������
        }
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime * 7.5f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // ���������, ������ �� � Gatito
        GatitoScript gatito = other.GetComponent<GatitoScript>();
        if (gatito != null)
        {
            // ���������, �������� �� ���� �������������
            BigGatitoScript armoredGatito = other.GetComponent<BigGatitoScript>();
            float finalDamage = damage;

            if (armoredGatito != null)
            {
                // ��������� ������ ����� (0.5f = 50% ������)
                finalDamage *= (1f - armoredGatito._armor);
            }

            // ������� ����
            gatito.TakeDamage(finalDamage);
        }

        // ���������� ���� � ����� ������
        Destroy(gameObject);
    }
}