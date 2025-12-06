using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения

    void Update()
    {
        // Получаем ввод с клавиатуры
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D
        float verticalInput = Input.GetAxis("Vertical");   // W/S

        // Создаем вектор движения
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        // Перемещаем персонажа
        transform.Translate(movement);
    }
}
