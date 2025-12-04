using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;

public class UnityServicesManager : MonoBehaviour
{
    // Используем Awake, чтобы инициализация произошла как можно раньше
    async void Awake()
    {
        await InitializeUnityServices();
    }

    async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");
        }
        catch (System.Exception e)
        {
            // Если инициализация не удалась, игра может работать локально, но без сохранения в облаке
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }
    }
}
