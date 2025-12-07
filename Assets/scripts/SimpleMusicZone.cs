using UnityEngine;

public class SimpleMusicZone : MonoBehaviour
{
    [Header("Настройки зоны")]
    public string zoneName = "Forest";
    public AudioClip zoneMusic;
    public float fadeTime = 1.5f;
    
    [Header("Размер триггера")]
    public Vector2 triggerSize = new Vector2(50f, 50f);
    
    void Start()
    {
        // Добавляем или находим BoxCollider2D
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        
        collider.isTrigger = true;
        collider.size = triggerSize;
        
        // Проверка
        if (zoneMusic == null)
        {
            Debug.LogError($"{gameObject.name}: Не назначена музыка для зоны!");
        }
        else
        {
            Debug.Log($"Зона '{zoneName}' готова, музыка: {zoneMusic.name}");
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем тег "Player" (у вашего игрока должен быть такой тег!)
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Игрок вошел в зону: {zoneName}");
            
            if (zoneMusic != null && MusicManager.Instance != null) // ← ИСПРАВЛЕНО: Instance с большой I
            {
                MusicManager.Instance.ChangeToMusic(zoneMusic, zoneName, fadeTime); // ← ИСПРАВЛЕНО
            }
            else
            {
                Debug.LogError($"Не могу сменить музыку! zoneMusic={zoneMusic != null}, Instance={MusicManager.Instance != null}"); // ← ИСПРАВЛЕНО
            }
        }
    }
    
    // Визуализация в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(transform.position, new Vector3(triggerSize.x, triggerSize.y, 1));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(triggerSize.x, triggerSize.y, 1));
    }
}