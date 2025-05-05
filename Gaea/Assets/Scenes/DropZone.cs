using UnityEngine;
using UnityEngine.SceneManagement; // For scene switching

public class DropZone : MonoBehaviour
{
    public string correctTag;
    public static int correctDrops = 0;
    public static int requiredDrops = 5;
    public int nextSceneIndex = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(correctTag))
        {
            Debug.Log("Correct bin!");
            Destroy(collision.gameObject);
            correctDrops++;
            Debug.Log("Correct Drops: " + correctDrops);

            if (correctDrops >= requiredDrops)
            {
                Debug.Log("Game Completed!");
                 Invoke("LoadNextScene", 1f);
                //SceneManager.LoadScene(nextSceneIndex);
            }
        }
        else
        {
            Debug.Log("Wrong bin!");
            collision.transform.position = collision.GetComponent<DragAndDrop>().originalPosition; // Reset position if dropped incorrectly
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex); // Load the next scene
    }
}
