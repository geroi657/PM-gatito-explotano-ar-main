using UnityEngine;

public class RegenGatitoScript : GatitoScript
{
    [Header("Регенерация")]
    [SerializeField] private float _regenPerSecond = 0.25f; // 25% от макс. здоровья в секунду
    [SerializeField] private float _regenInterval = 1f;     // Интервал между восстановлением

    private float _timeSinceLastHeal;

    protected override void Start()
    {
        _maxHealth = 250f; // Увеличенный запас здоровья
        base.Start();       // Инициализирует _currentHealth = _maxHealth
    }

    private void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(cameraPos);
        transform.position = Vector3.MoveTowards(transform.position, cameraPos, 0.5f * Time.deltaTime);

        if (!IsAlive) return; // Не восстанавливаем мёртвых котов

        _timeSinceLastHeal += Time.deltaTime;
        if (_timeSinceLastHeal >= _regenInterval)
        {
            HealOverTime();
            _timeSinceLastHeal = 0f;
        }
    }

    private void HealOverTime()
    {
        float healAmount = _maxHealth * _regenPerSecond;
        _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);
    }

    protected override void Die()
    {
        base.Die(); // Вызов родительского метода для взрыва и уничтожения
    }
}