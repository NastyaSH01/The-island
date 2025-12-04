using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;
    public Animator animator;
    
    private bool isMoving;
    private Vector3 targetPos;
    private Vector2 lastDirection;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 direction = GetDirectionInput();
            
            if (direction != Vector2.zero)
            {
                if (CanMove(direction))
                {
                    StartCoroutine(MoveToPosition(direction));
                }
            }
        }
        
        UpdateAnimations();
    }
    
    private Vector2 GetDirectionInput()
    {
        Vector2 direction = Vector2.zero;
        
        if (Input.GetKey(KeyCode.A)) direction = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) direction = Vector2.right;
        else if (Input.GetKey(KeyCode.W)) direction = Vector2.up;
        else if (Input.GetKey(KeyCode.S)) direction = Vector2.down;
        
        return direction;
    }
    
    private bool CanMove(Vector2 direction)
    {
        Vector2 newPosition = (Vector2)transform.position + direction * gridSize;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gridSize);
        
        return hit.collider == null;
    }
    
    private System.Collections.IEnumerator MoveToPosition(Vector2 direction)
    {
        isMoving = true;
        lastDirection = direction;
        targetPos = transform.position + (Vector3)(direction * gridSize);
        
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        
        while (elapsedTime < (1f / moveSpeed))
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime * moveSpeed);
            yield return null;
        }
        
        transform.position = targetPos;
        isMoving = false;
    }
    
    private void UpdateAnimations()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            
            if (!isMoving)
            {
                animator.SetFloat("LastMoveX", lastDirection.x);
                animator.SetFloat("LastMoveY", lastDirection.y);
            }
            else
            {
                animator.SetFloat("MoveX", lastDirection.x);
                animator.SetFloat("MoveY", lastDirection.y);
            }
        }
        
        if (lastDirection.x < 0) spriteRenderer.flipX = true;
        else if (lastDirection.x > 0) spriteRenderer.flipX = false;
    }
}
