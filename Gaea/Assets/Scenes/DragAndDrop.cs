using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 originalPosition;
    public CanvasGroup canvasGroup;
    public Camera mainCamera;
    public float snapThreshold = 0.5f;

    public bool isDragging = false;

    public static int correctDrops = 0;
    public static int requiredDrops = 5;
    public int nextSceneIndex = 20;

    void OnMouseDown()
    {
        isDragging = true;
        Debug.Log(gameObject.name + " selected");
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            //Debug.Log("Dragging: " + gameObject.name + " | Mouse: " + mousePosition);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        //Debug.Log(gameObject.name + " released");
    }

    private void Awake()
    {
        originalPosition = transform.position;
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        mainCamera = Camera.main;

        Debug.Log("CanvasGroup: " + canvasGroup);
        Debug.Log("Main Camera: " + mainCamera);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
      //  Debug.Log("Dragging Started: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0; // Keep the object in the 2D plane
        transform.position = newPosition;
        //Debug.Log("Dragging: " + newPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if dropped onto the correct bin
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        if (hitCollider != null && hitCollider.CompareTag("TrashBin"))
        {
            Debug.Log("Correct! Trash placed in the right bin.");

            correctDrops++;
            Debug.Log("Correct Drops: " + correctDrops);

            Destroy(gameObject);

            // Check if we've reached the required drops and then load the next scene
            if (correctDrops >= requiredDrops)
            {
                Debug.Log("Game Completed!");
                // Directly load the next scene (for debugging)
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
        else
        {
            Debug.Log("Incorrect! Resetting position.");
            transform.position = originalPosition; // Reset position if incorrect
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
