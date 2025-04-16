using System.Collections;
using UnityEngine;

public class miniMovement : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float moveDistanceX = 1.5f;
    public float moveDistanceY = 1.35f;
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private bool canMove = true;
    public int thirsty;
    public int moves = 0;
    public LayerMask obstacleLayer; // Assign this in the Inspector
    public LayerMask waterLayer;
    public LayerMask winLayer;
    //public int downCounter = 0;
    public bool nowUp = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!canMove) return;

        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) // Move up
        {
                    //DO THIS BASED ON SPRITE COORDINATES!!!!
            if (nowUp && transform.position.y >= -0.2) {
                nowUp = false;
                direction = new Vector2(0, 1.5f);
            } else {
                //direction = new Vector2(0, 1.48f);
                direction = new Vector2(0, moveDistanceY);
                Debug.Log("doing a big up move");
            }
            spriteRenderer.sprite = upSprite;
            /*if (transform.position.y <= -2){
                direction = new Vector2(0, 1.45f);
            } else {
                direction = new Vector2(0, moveDistanceY);
            } 
            */

            
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Move down
        {
            //DO THIS BASED ON SPRITE COORDINATES
            if (transform.position.y >= -0.4) {
                direction = new Vector2(0, -moveDistanceY);
            } else {
                direction = new Vector2(0, -1.48f);
                nowUp = true;
                Debug.Log("doing a big down move");
            }
            /*if (transform.position.y >= -0.6 && transform.position.y <= -0.4){
                direction = new Vector2(0, -1.45f);
            } else if (transform.position.y >= -2) {
                direction = new Vector2(0, -1.45f);
            } else {
                direction = new Vector2(0, -moveDistanceY);
            } */
            //spriteRenderer.sprite = upSprite;
            /*
            downCounter++;
            if (downCounter == 4){
                direction = new Vector2(0, -1.45f);
                downCounter = 0;
                nowUp = true;
            } else {
                direction = new Vector2(0, -moveDistanceY);
            }*/
            spriteRenderer.sprite = downSprite;    
        }

        else if (Input.GetKeyDown(KeyCode.A)) // Move left
        {
            if (transform.position.x >= -1.2 && transform.position.x <= -1) {
                direction = new Vector2(-1.68f, 0);
                Debug.Log("doing a big left move");
            } else {
                direction = new Vector2(-moveDistanceX, 0);
                
            }
            //direction = new Vector2(-moveDistanceX, 0);
            spriteRenderer.sprite = leftSprite;
        }

        else if (Input.GetKeyDown(KeyCode.D)) // Move right
        {
            if (transform.position.x >= -1.2 && transform.position.x <= -1) {
                direction = new Vector2(1.68f, 0);
                Debug.Log("doing a big right move");
            } else {
                direction = new Vector2(moveDistanceX, 0);
                
            }
            //direction = new Vector2(moveDistanceX, 0);
            spriteRenderer.sprite = rightSprite;
        }

        if (direction != Vector2.zero)
        {
            Vector2 targetPosition = (Vector2)transform.position + direction;

            if(justWon(targetPosition)){
                StartCoroutine(MovePlayer(direction, false, true));
            }
            else if (!IsObstacleAtPosition(targetPosition) && justHitWater(targetPosition))
            {
                Debug.Log("i am trying to land on water");
                StartCoroutine(MovePlayer(direction, true, false));
            } else if (!IsObstacleAtPosition(targetPosition) && !justHitWater(targetPosition))
            {
                //Debug.Log("block by rock");
                StartCoroutine(MovePlayer(direction, false, false));
            }
        }
    }

    private IEnumerator MovePlayer(Vector2 direction, bool waterHit, bool justWON)
    {
        canMove = false;
        if (moves < thirsty)
        {
            transform.position += (Vector3)direction;
            
            moves++;
            Debug.Log("just moved, moves = " + moves);
            if (waterHit){
                moves=0;
                Debug.Log("just moved onto water, moves = " + moves);
            }
            if (justWON){
                Debug.Log("JUST WON!!! GAME");
            }
            yield return new WaitForSeconds(1f);
        }
        canMove = true;
    }

    private bool IsObstacleAtPosition(Vector2 targetPosition)
    {
        return Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
    }
    private bool justHitWater(Vector2 targetPosition)
    {
        return Physics2D.OverlapCircle(targetPosition, 0.1f, waterLayer);//waterHit;
    }
    private bool justWon(Vector2 targetPosition)
    {
        return Physics2D.OverlapCircle(targetPosition, 0.1f, winLayer);//winHit;
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMovement : MonoBehaviour
{
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private bool canMove = true; // Movement cooldown flag
    public int thirsty;
    public int moves = 0;

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
        if (!canMove) return; // Prevent movement during cooldown

        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W) && upColliders.Count == 0) // Move up
        {
            direction.y = 1.35f;
            spriteRenderer.sprite = upSprite;
        }
        else if (Input.GetKeyDown(KeyCode.S) && downColliders.Count == 0) // Move down
        {
            direction.y = -1.35f;
            spriteRenderer.sprite = downSprite;
        }
        else if (Input.GetKeyDown(KeyCode.A) && leftColliders.Count == 0) // Move left
        {
            direction.x = -1.5f;
            spriteRenderer.sprite = leftSprite;
        }
        else if (Input.GetKeyDown(KeyCode.D) && rightColliders.Count == 0) // Move right
        {
            direction.x = 1.5f;
            spriteRenderer.sprite = rightSprite;
        }

        if (direction != Vector2.zero)
        {
            StartCoroutine(MovePlayer(direction));
        }
    }

    private IEnumerator MovePlayer(Vector2 direction)
    {
        canMove = false; // Disable movement
        if (moves >= thirsty){
            spriteRenderer.sprite = rightSprite;
        } else {
            transform.Translate(direction);
            moves = moves +1;
            yield return new WaitForSeconds(1f); // Wait 1 second
        }
        canMove = true; // Enable movement again
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
        leftColliders.Remove(collision.collider);
        rightColliders.Remove(collision.collider);
        upColliders.Remove(collision.collider);
        downColliders.Remove(collision.collider);
    }
}*/