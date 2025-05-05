using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public LayerMask obstacleLayer; 
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
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("EndPageTM");
                yield break;
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