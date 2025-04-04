using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bulletPrefab;  // Префаб пули
    public GameObject pelletPrefab; // Префаб дроби

    [Header("ShootSettings")]
    public float bulletSpeed = 10f;  // Скорость пули
    public float spreadAngle = 10f; // Угол отклона
    public int pelletAmount = 10; // Количество дробинок
    private bool isShotgunMode = false;

    [Header("Damage")]
    public float bulletDamage = 100f;
    public float pelletDamage = 25f;

    public void ToggleShootMode()
    {
        isShotgunMode = !isShotgunMode;
    }

    public void OnShootButtonPressed() {
        if (isShotgunMode) {
            ShootShotgun();
        }
        else {
            Shoot();
        }
    }


    public void Shoot()
    {
        // Берём актуальную камеру
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Создаём пулю в текущей позиции камеры с её направлением
            GameObject bulletInstance = Instantiate(
                bulletPrefab, 
                mainCamera.transform.position, 
                mainCamera.transform.rotation
            );

            /// Получаем компонент BulletScript и устанавливаем скорость
            BulletScript bulletScript = bulletInstance.GetComponent<BulletScript>();
            if (bulletScript != null) {
                bulletScript.SetVelocity(mainCamera.transform.forward * bulletSpeed);
                bulletScript.damage = bulletDamage;
            }

            // Уничтожаем пулю через 3 секунды
            Destroy(bulletInstance, 5f);
        }
        else
        {
            Debug.LogError("Главная камера не найдена!");
        }
    }

    public void ShootShotgun() {
        // Берём актуальную камеру
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            for (int i = 0; i < pelletAmount; i++) {
                // Создаём пулю в текущей позиции камеры с её направлением
                GameObject pelletInstance = Instantiate(
                    pelletPrefab, 
                    mainCamera.transform.position, 
                    mainCamera.transform.rotation
                );

                Vector3 randomDirection = GetRandomDirection(mainCamera.transform.forward);
                Vector3 pelletVelocity = randomDirection * bulletSpeed;

                BulletScript pelletScript = pelletInstance.GetComponent<BulletScript>();
                if (pelletScript != null) {
                    pelletScript.SetVelocity(pelletVelocity);
                    pelletScript.damage = pelletDamage;
                }

                // Уничтожаем пулю через 1.5 секунды
                Destroy(pelletInstance, 1.5f);
            }
        }
        else
        {
            Debug.LogError("Главная камера не найдена!");
        }
    }

    private Vector3 GetRandomDirection(Vector3 baseDirection)
    {
        // Генерируем случайные углы отклонения в пределах spreadAngle
        float randomAngleX = Random.Range(-spreadAngle, spreadAngle);
        float randomAngleY = Random.Range(-spreadAngle, spreadAngle);

        // Создаём случайное вращение
        Quaternion randomRotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);

        // Применяем вращение к базовому направлению
        return randomRotation * baseDirection;
    }
}
