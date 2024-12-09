using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;

    private HashSet<Collider2D> leftColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> rightColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> upColliders = new HashSet<Collider2D>();
    private HashSet<Collider2D> downColliders = new HashSet<Collider2D>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && upColliders.Count == 0) // Move up
        {
            direction.y = 1;
            spriteRenderer.sprite = upSprite;
        }
        else if (Input.GetKey(KeyCode.S) && downColliders.Count == 0) // Move down
        {
            direction.y = -1;
            spriteRenderer.sprite = downSprite;
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
        transform.Translate(direction * speed * Time.deltaTime);
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
