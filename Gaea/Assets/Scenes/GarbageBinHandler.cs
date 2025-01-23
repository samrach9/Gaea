using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class GarbageHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collided with a trash can
        if (collision.CompareTag("RecycleBin") || collision.CompareTag("CompostBin") || collision.CompareTag("TrashBin"))
        {
            Debug.Log($"{gameObject.name} dropped into {collision.tag} bin!");

            // Optional: Destroy the garbage object
            Destroy(gameObject);
        }
    }
}
