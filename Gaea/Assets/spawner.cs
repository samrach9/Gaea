using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float[] lanes;
    public float spawnX = 10f;
    public float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        float y = lanes[Random.Range(0, lanes.Length)];
        Vector3 spawnPosition = new Vector3(spawnX, y, 1f);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
