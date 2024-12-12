using UnityEngine;

public class CollisionOutline : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    
    // Color and width of the outline
    [Header("Outline Settings")]
    [SerializeField] private Color outlineColor = Color.red;
    [SerializeField] private float outlineWidth = 0.1f;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Ensure we have a SpriteRenderer
        if (spriteRenderer == null)
        {
            Debug.LogError("CollisionOutline script requires a SpriteRenderer component!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Add red outline when collision occurs
        if (spriteRenderer != null)
        {
            spriteRenderer.material.SetFloat("_OutlineWidth", outlineWidth);
            spriteRenderer.material.SetColor("_OutlineColor", outlineColor);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Remove outline when collision ends
        if (spriteRenderer != null)
        {
            spriteRenderer.material.SetFloat("_OutlineWidth", 0f);
        }
    }
}