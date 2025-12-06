using UnityEngine;

public class InteractiveBoatRepair : MonoBehaviour
{
    // Префаб целой лодки
    public GameObject repairedBoatPrefab;

    // Тег предмета, который нужно принести (например, "RepairKit")
    public string requiredItemTag = "RepairKit";

    // Максимальное расстояние для срабатывания ремонта (установите в Инспекторе)
    public float repairDistanceThreshold = 1.5f;

    // Ссылка на найденный ремонтный предмет
    private GameObject nearbyRepairItem = null;

    void Update()
    {
        // Если предмет еще не найден, пытаемся его найти в сцене по тегу
        if (nearbyRepairItem == null)
        {
            FindRepairItem();
            return; // Пропускаем этот кадр, если предмет не найден
        }

        // Если предмет найден, проверяем расстояние до него
        float distance = Vector3.Distance(transform.position, nearbyRepairItem.transform.position);

        if (distance <= repairDistanceThreshold)
        {
            Debug.Log("Предмет достаточно близко! Лодка чинится.");
            RepairBoat();
        }
    }

    void FindRepairItem()
    {
        // Ищем все объекты с нужным тегом в сцене
        GameObject[] items = GameObject.FindGameObjectsWithTag(requiredItemTag);

        // В простейшем случае берем первый найденный предмет
        // (Можно улучшить, если у вас много таких предметов)
        if (items.Length > 0)
        {
            nearbyRepairItem = items[0];
        }
    }

    void RepairBoat()
    {
        // Уничтожаем использованный ремонтный предмет
        if (nearbyRepairItem != null)
        {
            Destroy(nearbyRepairItem);
        }

        // Создаем целую лодку
        Instantiate(repairedBoatPrefab, transform.position, transform.rotation);

        // Уничтожаем сломанную лодку
        Destroy(gameObject);
    }
}
