using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpawnScript : MonoBehaviour
{
    public const float DEFAULT_OFFSET = 3.5f;
    public const float SPAWN_ANGLE_RANGE = 30f;

    [Header("Префабы")]
    public GameObject basicCatPrefab;    // Обычный гатито (GatitoScript)
    public GameObject armoredCatPrefab;  // Бронированный (BigGatitoScript)
    public GameObject regenCatPrefab;    // С регенерацией (RegenGatitoScript)
    public GameObject giftPrefab;
    public GameObject metalPipePrefab;

    [Header("Настройки спавна")]
    [Range(0f, 1f)] public float armoredCatChance = 0.3f;
    [Range(0f, 1f)] public float regenCatChance = 0.2f;
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomEntity();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomEntity()
    {
        Vector3 spawnPos = CalculateSpawnPosition();

        float rand = Random.value;
        if (rand > 0.9f) // 10% шанс спавна подарка/трубы
        {
            Instantiate(Random.value > 0.5f ? giftPrefab : metalPipePrefab, spawnPos, Quaternion.identity);
        }
        else // Спавн гатито
        {
            GameObject catToSpawn = SelectCatType();
            Instantiate(catToSpawn, spawnPos, Quaternion.identity);
        }
    }

    Vector3 CalculateSpawnPosition()
    {
        float angle = Random.Range(-SPAWN_ANGLE_RANGE, SPAWN_ANGLE_RANGE);
        float sx = DEFAULT_OFFSET * Mathf.Sin(angle * Mathf.Deg2Rad);
        float sy = Random.Range(-0.5f, 0.5f);
        float sz = DEFAULT_OFFSET * Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector3(sx, sy, sz);
    }

    GameObject SelectCatType()
    {
        float rand = Random.value;

        if (rand < regenCatChance) // Сначала проверяем редкие типы
            return regenCatPrefab;
        if (rand < armoredCatChance + regenCatChance)
            return armoredCatPrefab;

        return basicCatPrefab; // Обычный гатито по умолчанию
    }
}
