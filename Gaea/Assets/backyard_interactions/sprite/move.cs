using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 5f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) // Move up
        {
            direction.y = 1;
            spriteRenderer.sprite = upSprite;
        }
        else if (Input.GetKey(KeyCode.S)) // Move down
        {
            direction.y = -1;
            spriteRenderer.sprite = downSprite;
        }
        
        if (Input.GetKey(KeyCode.A)) // Move left
        {
            direction.x = -1;
            spriteRenderer.sprite = leftSprite;
        }
        else if (Input.GetKey(KeyCode.D)) // Move right
        {
            direction.x = 1;
            spriteRenderer.sprite = rightSprite;
        }

        // Normalize direction to ensure consistent speed in diagonals
        direction.Normalize();
        
        // Move the player
        transform.Translate(direction * speed * Time.deltaTime);
    }
}