using UnityEngine;

public class SpriteLocation : MonoBehaviour
{
    void Start()
    {
        // Get the position of the GameObject this script is attached to
        Vector3 spritePosition = transform.position;

        // Print the position to the console
        Debug.Log("Sprite Position: " + spritePosition);
    }
}