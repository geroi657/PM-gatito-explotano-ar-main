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
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Для точных столкновений
        }
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime * 7.5f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, попали ли в Gatito
        GatitoScript gatito = other.GetComponent<GatitoScript>();
        if (gatito != null)
        {
            // Проверяем, является ли цель бронированной
            BigGatitoScript armoredGatito = other.GetComponent<BigGatitoScript>();
            float finalDamage = damage;

            if (armoredGatito != null)
            {
                // Применяем защиту брони (0.5f = 50% защиты)
                finalDamage *= (1f - armoredGatito._armor);
            }

            // Наносим урон
            gatito.TakeDamage(finalDamage);
        }

        // Уничтожаем пулю в любом случае
        Destroy(gameObject);
    }
}