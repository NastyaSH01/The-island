using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        // Добавлен суффикс 'f' к числам, чтобы указать, что это float
        Vector2 playerPos = new Vector2(player.position.x - 0.3f, player.position.y + 0.3f);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
