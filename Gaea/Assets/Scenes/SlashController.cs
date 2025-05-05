using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SlashDetector : MonoBehaviour
{
    private Vector3 lastMousePosition;
    public float minSlashSpeed = 5f;
    public GameObject slashEffectPrefab; // Reference to the slash effect
    public int invSlashedCount = 0;
    public int nextSceneIndex = 2;

    void Update()
    {
        if (Input.GetMouseButton(0)) // Detects mouse drag or touch
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;

            if (Vector3.Distance(lastMousePosition, currentMousePosition) > minSlashSpeed * Time.deltaTime)
            {
                CreateSlashEffect(currentMousePosition);
                CheckSlash(currentMousePosition);
            }

            lastMousePosition = currentMousePosition;
        }
    }

    void CheckSlash(Vector3 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 0.5f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Invasive"))
            {
                Destroy(collider.gameObject); // Remove invasive species
                Debug.Log("Invasive plant slashed!");
                invSlashedCount++;
                if (invSlashedCount == 5){
                   Debug.Log("Game Completed!");
                   Invoke("LoadNextScene", 1f);
                }
            }
            else if (collider.CompareTag("Real"))
            {
                Destroy(collider.gameObject);
                Debug.Log("Wrong plant! Penalty applied.");
            }
        }
    }

    void CreateSlashEffect(Vector3 position)
    {
        if (slashEffectPrefab != null)
        {
            GameObject effect = Instantiate(slashEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 0.3f); // Destroy effect after a short time
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex); // Load the next scene
    }
}
