using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно добавьте эту строку

public class LevelLoader : MonoBehaviour
{
    // Публичная функция для загрузки сцены по имени
    public void LoadNextScene()
    {
        // Загружаем следующую сцену, которая идет сразу после текущей в Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    // Альтернативная функция для загрузки по имени, если нужно указать конкретную сцену
    public void LoadSpecificScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

