using UnityEngine;

public class BigGatitoScript : GatitoScript
{
    [Header("Armor")]
    [Range(0, 0.9f), SerializeField]
    public float _armor = 0.5f;

    protected override void Start()
    {
        _maxHealth = 300f; // Увеличенное здоровье
        base.Start(); // Инициализация _currentHealth
    }

    public override void TakeDamage(float damage)
    {
        if (!IsAlive) return;

        float reducedDamage = damage * (1f - _armor);

        base.TakeDamage(reducedDamage); // Вызываем родительский метод
    }

    protected override void Die()
    {
        base.Die(); // Вызываем родительский Die()
    }
}