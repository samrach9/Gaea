using UnityEngine;

public class Plant : MonoBehaviour
{
    public bool isInvasive; // Determines if the plant should be slashed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slash"))
        {
            if (isInvasive)
            {
                InvasiveGameManager.instance.InvasiveSlashed();
                Destroy(gameObject);
            }
            else
            {
                InvasiveGameManager.instance.RealPlantSlashed(); 
            }
        }
    }
}
