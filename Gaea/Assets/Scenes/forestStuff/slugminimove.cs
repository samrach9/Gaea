/*using System.Collections;
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
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class slugminimove : MonoBehaviour
{
    public float[] lanes; // assign in Inspector
    private int currentLane = 1; // starting lane index
    public float laneSwitchSpeed = 10f;
    public int score = 0; // optional score tracking
    public TMP_Text      scoreText;
    void Start(){
        UpdateScoreText();
    }
    

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
            prefabmove obstacle = other.GetComponent<prefabmove>();
            if (obstacle != null)
            {
                if (obstacle.IsBad())
                {
                    Debug.Log("Game Over!");
                    Time.timeScale = 0f;
                }
                else
                {
                    score++;
                    UpdateScoreText();
                    Debug.Log("Scored! Total: " + score);

                    Destroy(other.gameObject); // remove the "good" obstacle
                    if (score>=10){
                        StartCoroutine(HandleWin());
                    }
                }
            }
        }
    }
    IEnumerator HandleWin()
    {
        Debug.Log("JUST WON!!! GAME");
        yield return new WaitForSeconds(1f);

        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Mini LastScene", currentSceneName);
        PlayerPrefs.Save();

        SceneManager.LoadScene("bananaEND");
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
