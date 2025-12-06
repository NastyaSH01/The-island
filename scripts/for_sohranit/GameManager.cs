using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Core; 

public class GameManager : MonoBehaviour
{
    public GameObject player; 
    public int currentCoins = 0;
    public int currentLevelID = 1;
    public float moveSpeed = 5f; 

    private Rigidbody2D playerRb; 

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }

        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb == null)
            {
                Debug.LogError("Player object is missing Rigidbody2D component!");
            }
        }
    }

    async void Start()
    {
        await Task.Delay(1000); 
        await LoadGameData();
    }

    void Update()
    {
        if (playerRb != null)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveX, moveY);
            playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // Исправлено: игнорируем задачу, чтобы убрать предупреждение CS4014
            var unusedSaveTask = SaveGameData(); 
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Исправлено: игнорируем задачу, чтобы убрать предупреждение CS4014
            var unusedLoadTask = LoadGameData();
        }
    }
    
    private void ApplyLoadedDataToPlayer(PlayerProgress progress)
    {
        if (progress != null && playerRb != null)
        {
            currentCoins = progress.coins;
            currentLevelID = progress.currentLevelID;
            playerRb.position = progress.GetPlayerPosition();
            Debug.Log("Game state updated with cloud data successfully.");
        }
    }

    public async Task SaveGameData()
    {
        Vector2 playerPos = (playerRb != null) ? playerRb.position : (Vector2)player.transform.position;
        PlayerProgress progress = new PlayerProgress(currentCoins, currentLevelID, playerPos);

        if (CloudSaveManager.Instance != null)
        {
            await CloudSaveManager.Instance.SavePlayerDataAsync(progress);
        }
    }

    public async Task LoadGameData()
    {
        if (CloudSaveManager.Instance != null)
        {
            PlayerProgress progress = await CloudSaveManager.Instance.LoadPlayerDataAsync();

            if (progress != null)
            {
                ApplyLoadedDataToPlayer(progress);
            }
        }
    }
}
