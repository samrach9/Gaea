using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class RightButtonSwap : MonoBehaviour
{
    [Header("References")]
    public Button button; // The button that triggers the sprite change
    public SpriteRenderer targetSpriteRenderer; // The sprite renderer of the target component

    [Header("Sprites")]
    public Sprite newSprite; // The sprite to switch to
    public Sprite originalSprite; // The original sprite to switch back to (can be null)

    private bool isUsingNewSprite = false; // Tracks which sprite is currently being used

    void Start()
    {
        // Ensure references are set
        if (button == null || targetSpriteRenderer == null || newSprite == null)
        {
            Debug.LogError("Please assign all references and the new sprite in the inspector.");
            return;
        }

        // Add a listener to the button to call ToggleSprite when clicked
        button.onClick.AddListener(ToggleSprite);
    }

    void ToggleSprite()
    {
        // Toggle between the original sprite (or none) and the new sprite
        if (targetSpriteRenderer != null)
        {
            if (isUsingNewSprite)
            {
                targetSpriteRenderer.sprite = originalSprite; // This can be null
            }
            else
            {
                targetSpriteRenderer.sprite = newSprite;
            }

            isUsingNewSprite = !isUsingNewSprite;
            Debug.Log("Sprite toggled successfully!");
        }
        else
        {
            Debug.LogWarning("SpriteRenderer is not assigned.");
        }
    }
}
