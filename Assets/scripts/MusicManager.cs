using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [Header("Основные настройки")]
    public AudioSource audioSource;
    public AudioClip defaultMusic;
    
    [Header("Отладка")]
    [SerializeField] private string currentZone = "Default";
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void SetupAudio()
    {
        Debug.Log("=== MUSIC MANAGER ===");
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Настройка AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.spatialBlend = 0f;
        
        // Запускаем музыку по умолчанию
        if (defaultMusic != null)
        {
            StartMusic(defaultMusic, "Default", 0f);
            Debug.Log($"Запущена музыка по умолчанию: {defaultMusic.name}");
        }
        else
        {
            Debug.LogError("Не назначена defaultMusic!");
        }
    }
    
    // Публичный метод для смены музыки (будут вызывать зоны)
    public void ChangeToMusic(AudioClip newClip, string zoneName, float fadeTime = 1f)
    {
        if (newClip == null)
        {
            Debug.LogWarning($"Попытка сменить на null клип из зоны {zoneName}");
            return;
        }
        
        if (audioSource.clip == newClip)
        {
            Debug.Log($"Уже играет музыка для зоны {zoneName}");
            return;
        }
        
        Debug.Log($"Смена музыки на зону: {zoneName}, трек: {newClip.name}");
        StartCoroutine(FadeToNewMusic(newClip, zoneName, fadeTime));
    }
    
    // Внутренний метод для запуска музыки
    private void StartMusic(AudioClip clip, string zoneName, float fadeTime)
    {
        if (clip == null) return;
        
        currentZone = zoneName;
        audioSource.clip = clip;
        audioSource.Play();
        
        if (fadeTime > 0)
        {
            StartCoroutine(FadeInMusic(fadeTime));
        }
        else
        {
            audioSource.volume = 1f;
        }
    }
    
    private IEnumerator FadeToNewMusic(AudioClip newClip, string zoneName, float fadeTime)
    {
        currentZone = zoneName;
        
        // Если уже играет музыка - делаем фейд-аут
        if (audioSource.isPlaying && audioSource.clip != null && fadeTime > 0)
        {
            float startVolume = audioSource.volume;
            
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
                yield return null;
            }
            
            audioSource.Stop();
        }
        
        // Меняем клип
        audioSource.clip = newClip;
        audioSource.Play();
        
        // Фейд-ин новой музыки
        if (fadeTime > 0)
        {
            float targetVolume = 1f;
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeTime);
                yield return null;
            }
        }
        
        audioSource.volume = 1f;
        Debug.Log($"✅ Музыка изменена: {zoneName} - {newClip.name}");
    }
    
    private IEnumerator FadeInMusic(float fadeTime)
    {
        audioSource.volume = 0f;
        audioSource.Play();
        
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, 1f, t / fadeTime);
            yield return null;
        }
        
        audioSource.volume = 1f;
    }
    
    // Для отладки
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log($"Текущая зона: {currentZone}, Трек: {audioSource.clip?.name}, isPlaying: {audioSource.isPlaying}");
        }
    }
}