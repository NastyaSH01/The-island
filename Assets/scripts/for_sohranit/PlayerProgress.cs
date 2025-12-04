using UnityEngine; // <--- Добавьте эту строку

[System.Serializable]
public class PlayerProgress
{
    public int coins;
    public int currentLevelID;
    // Сохраняем позицию как массив из 2-х чисел (x, y) для простоты сериализации
    public float[] playerPosition = new float[2]; 

    // Конструктор для удобного создания объекта сохранения
    public PlayerProgress(int c, int lvl, Vector2 pos)
    {
        coins = c;
        currentLevelID = lvl;
        playerPosition[0] = pos.x;
        playerPosition[1] = pos.y;
    }

    public Vector2 GetPlayerPosition()
    {
        return new Vector2(playerPosition[0], playerPosition[1]);
    }
}

