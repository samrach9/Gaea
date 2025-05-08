using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject invasivePlantPrefab;
    public GameObject realPlantPrefab;
    public Transform spawnArea;
    public float spawnInterval = 0.75f;
    public float launchForce = 5f;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnInterval)
        {
            SpawnPlant();
            timeElapsed = 0f;
        }
    }

    void SpawnPlant()
    {
        GameObject plantPrefab = Random.value > 0.5f ? invasivePlantPrefab : realPlantPrefab;

        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), -5f, 0f);
        GameObject plant = Instantiate(plantPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = plant.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float randomForce = Random.Range(launchForce * 0.8f, launchForce * 1.2f); // Randomize slightly
            float randomDirection = Random.Range(-1f, 1f); // Slight curve
            rb.velocity = new Vector2(randomDirection, randomForce);
        }
    }
}
