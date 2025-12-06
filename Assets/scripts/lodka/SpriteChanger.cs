using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour
{
    public Sprite newSprite; 
    private Sprite originalSprite; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    // Эта функция вызывается, когда ОБЪЕКТ-ТРИГГЕР (кидаемый предмет)
    // входит в зону нашего обычного коллайдера
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // other - это коллайдер кидаемого предмета
        if (other.GetComponent<TriggerObjectIdentifier>() != null)
        {
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    // Эта функция вызывается, когда ОБЪЕКТ-ТРИГГЕР выходит
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<TriggerObjectIdentifier>() != null)
        {
            if (spriteRenderer != null && originalSprite != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }
}
