using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class TreeMiniGame : MonoBehaviour
{
    [Header("References")]
    public Button button; // The button that triggers the scene change
    public SpriteRenderer RightPageObject; // The sprite renderer of the target object
    public int scenenum; // The scene to load

    [Header("Sprites")]
    public Sprite TreeSprite; // The specific sprite to check for

    void Start()
    {
        // Validate references to avoid runtime errors
        if (button == null || RightPageObject == null || TreeSprite == null)
        {
            Debug.LogError("Please assign all references in the inspector.");
            return;
        }

        // Add button listener
        button.onClick.AddListener(OnButtonPress);
    }

    void OnButtonPress()
    {
        // Check if the sprite matches the specific one
        if (RightPageObject.sprite == TreeSprite)
        {
            SceneManager.LoadScene(scenenum);
        }
        else
        {
            Debug.Log("The sprite does not match the required one.");
        }
    }
}