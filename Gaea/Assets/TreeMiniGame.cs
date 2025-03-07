using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class TreeMiniGame : MonoBehaviour
{
    [Header("References")]
    public Button button; // The button that triggers the scene change
    public SpriteRenderer RightPageObject; // The sprite renderer of the target object
    public int scenenum; // The scene to load
    public SpriteRenderer player;

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
            //player.transform.position = new Vector3(-7.05f, 3.38f, player.transform.position.z);
            
            //move sprite's location here!!
        }
        else
        {
            Debug.Log("The sprite does not match the required one.");
        }
    }
}
//7.05-5.41 = 164
//3.38-2.09 = 1.29