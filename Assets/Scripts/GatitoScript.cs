using System.Xml.Linq;
using UnityEngine;

public class GatitoScript : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject explode;

    protected float _maxHealth = 100f;
    protected float _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {


        if (!IsAlive) return;
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(cameraPos);
        transform.position = Vector3.MoveTowards(transform.position, cameraPos, 0.5f * Time.deltaTime);
    }

    protected virtual void Die()
    {
        if (explode != null)
        {
            Vector3 hitPoint = GetComponent<Collider>().ClosestPoint(transform.position);
            Instantiate(explode, hitPoint, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BulletScript>(out var bullet))
        {
            TakeDamage(bullet.damage);
            Destroy(other.gameObject);
        }
    }
}