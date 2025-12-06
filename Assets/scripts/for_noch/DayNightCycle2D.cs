using UnityEngine;
using UnityEngine.Rendering.Universal; // Необходим для доступа к Light2D

public class DayNightCycle2D : MonoBehaviour
{
    public Light2D globalLight; // Перетащите сюда ваш Global Light 2D из инспектора
    public float secondsPerDay = 60f; // Длительность одного цикла дня/ночи в секундах
    [Tooltip("Цвета освещения для разных времен суток (рассвет, день, закат, ночь)")]
    public Gradient dayNightColor; // Градиент для плавного перехода цветов

    private float _currentTimeOfDay = 0f; // Текущее время в диапазоне [0, 1]
    private float _timeMultiplier;

    void Start()
    {
        _timeMultiplier = 1f / secondsPerDay;
    }

    void Update()
    {
        // Постоянно обновляем время
        _currentTimeOfDay += Time.deltaTime * _timeMultiplier;

        // Если цикл завершен (достиг 1.0), начинаем сначала
        if (_currentTimeOfDay >= 1f)
        {
            _currentTimeOfDay = 0f;
        }

        // Применяем цвет из градиента в зависимости от текущего времени
        UpdateLighting();
    }

    void UpdateLighting()
    {
        // Оцениваем цвет градиента в текущей точке цикла [0, 1]
        Color lightColor = dayNightColor.Evaluate(_currentTimeOfDay);
        globalLight.color = lightColor;
        
        // Также можно регулировать интенсивность света
        // Например, ночью она может быть ниже
        if (_currentTimeOfDay >= 0.75f || _currentTimeOfDay <= 0.25f) // Условная ночь
        {
            globalLight.intensity = 0.5f; 
        }
        else // Условный день
        {
            globalLight.intensity = 1f;
        }
    }
}

