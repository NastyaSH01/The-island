using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput < 0)
        {
            // Устанавливаем масштаб X в -1 (отражаем влево)
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalInput > 0)
        {
            // Устанавливаем масштаб X в 1 (смотрим вправо)
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
