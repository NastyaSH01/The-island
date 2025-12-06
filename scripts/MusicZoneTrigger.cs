using UnityEngine;

public class MusicZoneTrigger : MonoBehaviour
{
    private AudioSource zoneMusic;
    // Ссылка на источник фоновой музыки (если есть)
    public AudioSource backgroundMusic; 

    void Start()
    {
        // Получаем AudioSource, прикрепленный к этому же объекту (MusicZone)
        zoneMusic = GetComponent<AudioSource>(); 
    }

    // Вызывается, когда другой коллайдер входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошел именно игрок (у игрока должен быть тег "Player")
        if (other.CompareTag("Player"))
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop(); // Останавливаем фоновую музыку
            }
            zoneMusic.Play(); // Запускаем музыку зоны
        }
    }

    // Вызывается, когда другой коллайдер выходит из триггера
    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что вышел именно игрок
        if (other.CompareTag("Player"))
        {
            zoneMusic.Stop(); // Останавливаем музыку зоны
            if (backgroundMusic != null)
            {
                backgroundMusic.Play(); // Запускаем фоновую музыку обратно (если она была)
            }
        }
    }
}

