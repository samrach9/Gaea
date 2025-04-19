using UnityEngine;

public class DropZone : MonoBehaviour
{
    public string correctTag; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(correctTag))
        {
            Debug.Log("Correct bin!");
            Destroy(collision.gameObject); // Dispose of the trash item
        }
        else
        {
            Debug.Log("Wrong bin!");
            collision.transform.position = collision.GetComponent<DragAndDrop>().originalPosition; // Reset position
        }
    }
}
