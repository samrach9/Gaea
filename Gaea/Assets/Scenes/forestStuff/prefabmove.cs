using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabmove : MonoBehaviour
{
    public float speed = 5f;
    
    public Sprite[] obstacleSprites;      // Assign in Inspector
    public bool[] isBadSprite;            // Match index to sprite array

    private SpriteRenderer sr;
    private bool isBad = true; // default

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int index = Random.Range(0, obstacleSprites.Length);
        sr.sprite = obstacleSprites[index];
        isBad = isBadSprite[index];
    }

    public bool IsBad()
    {
        return isBad;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -10f) // off screen
            Destroy(gameObject);
    }
}