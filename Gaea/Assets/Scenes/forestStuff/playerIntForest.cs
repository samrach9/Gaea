using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIntForest : MonoBehaviour
{
    public Sprite collisionSprite; // The sprite to switch to
    public GameObject targetObject; // The object whose sprite will be changed

    private SpriteRenderer targetRenderer;
    private Sprite originalSprite;

    private Vector3 originalScale;

    void Start()
    {
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (targetRenderer != null)
            {
                originalSprite = targetRenderer.sprite;
            }

            originalScale = targetObject.transform.localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "slurg" && targetRenderer != null)
        {
            targetRenderer.sprite = collisionSprite;
            targetObject.transform.localScale = originalScale * 0.1f; // 10% of original size
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "slurg" && targetRenderer != null)
        {
            targetRenderer.sprite = originalSprite;
            targetObject.transform.localScale = originalScale; // Restore size
        }
    }
}
