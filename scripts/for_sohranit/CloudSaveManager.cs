using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.CloudSave;
// using Newtonsoft.Json; // Эту строку убираем

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- Метод Сохранения ---
    public async Task SavePlayerDataAsync(PlayerProgress progressData)
    {
        const string KEY = "PLAYER_PROGRESS_DATA"; 

        // Сериализуем объект в строку JSON с помощью встроенного JsonUtility
        string json = JsonUtility.ToJson(progressData);

        var dataToSave = new Dictionary<string, object>
        {
            { KEY, json } // Сохраняем строку
        };

        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);
            Debug.Log("Game data (Progress, Position, Coins) successfully saved to the cloud.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save data to cloud: {e.Message}");
        }
    }

    // --- Метод Загрузки ---
    public async Task<PlayerProgress> LoadPlayerDataAsync()
    {
        const string KEY = "PLAYER_PROGRESS_DATA";
        
        try
        {
            var loadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { KEY });

            if (loadedData.ContainsKey(KEY))
            {
                // Десериализуем строку JSON обратно в наш класс PlayerProgress
                string json = loadedData[KEY].ToString();
                PlayerProgress progress = JsonUtility.FromJson<PlayerProgress>(json);
                
                Debug.Log("Game data successfully loaded from the cloud.");
                return progress;
            }
            else
            {
                Debug.Log("No saved data found in the cloud for this player.");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load data from cloud: {e.Message}");
            return null;
        }
    }
}
