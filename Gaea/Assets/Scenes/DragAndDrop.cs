using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 originalPosition;
    public CanvasGroup canvasGroup;
    public Camera mainCamera;
    public float snapThreshold = 0.5f;  // Distance threshold to snap to the bin

    public bool isDragging = false;

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
            Debug.Log("Dragging: " + gameObject.name + " | Mouse: " + mousePosition);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Debug.Log(gameObject.name + " released");
    }

    private void Awake()
    {
        originalPosition = transform.position;
        canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Allows UI interaction
        mainCamera = Camera.main; // Get the main camera
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;  // Make object slightly transparent when dragging
        canvasGroup.blocksRaycasts = false; // Allow objects underneath to detect raycasts
        Debug.Log("Dragging Started: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Convert to world position
        newPosition.z = 0; // Keep the object in the 2D plane
        transform.position = newPosition;
        Debug.Log("Dragging: " + newPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;  // Restore visibility
        canvasGroup.blocksRaycasts = true; // Re-enable raycasting

        // Check if the object was dropped near a bin with the "TrashBin" tag
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);  // Detect collision at the drop position
        if (hitCollider != null && hitCollider.CompareTag("TrashBin"))
        {
            // If the dropped object is near the bin and has the correct tag
            Debug.Log("Correct! Trash placed in the right bin.");
            // Optionally, snap to the bin position or do something else
            transform.position = hitCollider.transform.position;
        }
        else
        {
            // If not in the correct bin, reset position
            Debug.Log("Incorrect! Resetting position.");
            transform.position = originalPosition;
        }
    }
}
