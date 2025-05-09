using System.Collections.Generic;
using UnityEngine;

public class forestplayermove : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 20f;
    private SpriteRenderer spriteRenderer;
    //private Vector3 targetScale;
    //public float scaleSpeed = 5f; // How quickly it changes scale
    //private Vector3 normalScale = new Vector3(0.15f, 0.15f, 1f);
    //private Vector3 shrinkScale = new Vector3(0.1f, 0.1f, 1f);


    private HashSet<Collider2D> leftColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> rightColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> upColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> downColliders = new HashSet<Collider2D>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //targetScale = transform.localScale;
        //targetScale = transform.localScale = normalScale;
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && upColliders.Count == 0) // Move up
        {
            direction.y = 1;
            spriteRenderer.sprite = upSprite;
            //targetScale = new Vector3(0.97f, 0.97f, 1f);
            //targetScale = shrinkScale;
        }
        else if (Input.GetKey(KeyCode.S) && downColliders.Count == 0) // Move down
        {
            direction.y = -1;
            spriteRenderer.sprite = downSprite;
            //targetScale = normalScale;
        } 

        if (Input.GetKey(KeyCode.A) && leftColliders.Count == 0) // Move left
        {
            direction.x = -1;
            spriteRenderer.sprite = leftSprite;
        }
        else if (Input.GetKey(KeyCode.D) && rightColliders.Count == 0) // Move right
        {
            direction.x = 1;
            spriteRenderer.sprite = rightSprite;
        }

        direction.Normalize();
        // Smooth scale transition
        /*transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        if (Vector3.Distance(transform.localScale, targetScale) < 0.001f)
        {
            transform.localScale = targetScale;
        }*/

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;

        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        {
            if (normal.x > 0)
                leftColliders.Add(collision.collider);
            else
                rightColliders.Add(collision.collider);
        }
        else
        {
            if (normal.y > 0)
                downColliders.Add(collision.collider);
            else
                upColliders.Add(collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove colliders from direction-specific sets
        leftColliders.Remove(collision.collider);
        rightColliders.Remove(collision.collider);
        upColliders.Remove(collision.collider);
        downColliders.Remove(collision.collider);
    }
}
