using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabmove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -10f) // off screen
            Destroy(gameObject);
    }
}