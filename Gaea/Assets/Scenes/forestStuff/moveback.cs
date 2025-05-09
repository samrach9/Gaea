using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class moveback : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundWidth;
    
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float backgroundWidth = sr.bounds.size.x;  // accurate in world units
        Debug.Log(backgroundWidth);
    }

    void Update()
    {
        // Always move left, regardless of local scale
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // When the sprite goes off screen, loop it
        if (transform.position.x < -backgroundWidth)
        {
            transform.position += new Vector3(backgroundWidth * 2f, 0f, 0f);
        }
    }
}
