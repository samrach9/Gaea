using UnityEngine;

public class trashGenerator : MonoBehaviour
{
    public GameObject trash;

    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    public float SpeedMultiplier; 

    // Start is called before the first frame update
    void Awake()
    {
        currentSpeed = MinSpeed;
        generateTrash();
    }

    public void GenerateNextTrashGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke ("generateTrash", randomWait);
    }

    void generateTrash()
    {
        GameObject TrashIns = Instantiate(trash, transform.position, transform.rotation);

        TrashIns.GetComponent<TrashScript>().trashGenerator = this; 
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < MaxSpeed)
        {
            currentSpeed += SpeedMultiplier; 
        }
    }
}
