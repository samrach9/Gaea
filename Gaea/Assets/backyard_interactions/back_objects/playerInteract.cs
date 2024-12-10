using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    public Sprite collisionSprite;

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original sprite (null if there's no sprite initially)
        originalSprite = spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When collision starts, set the collision sprite
        if (collisionSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = collisionSprite;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // When collision ends, revert to the original sprite (or none if it was initially null)
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }
}
