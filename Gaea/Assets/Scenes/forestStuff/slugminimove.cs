using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugminimove : MonoBehaviour
{
    public float[] lanes; // assign in Inspector
    private int currentLane = 1; // starting lane index
    public float laneSwitchSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLane > 0)
            currentLane--;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && currentLane < lanes.Length - 1)
            currentLane++;

        Vector3 targetPosition = new Vector3(transform.position.x, lanes[currentLane], transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0f; 
        }
    }
}
