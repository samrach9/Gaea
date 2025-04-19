using UnityEngine;
using UnityEngine.UI;

public class InvasiveGameManager : MonoBehaviour
{
    public static InvasiveGameManager instance;
    public Text scoreText;
    public Text timerText;
    public float gameDuration = 30f;

    private int score = 0;
    private float timeRemaining;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        timeRemaining = gameDuration;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    public void InvasiveSlashed()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score >= 5)
        {
            WinGame();
        }
    }

    public void RealPlantSlashed()
    {
        score--;
        scoreText.text = "Score: " + score;
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        // Show Game Over UI or restart option
    }

    private void WinGame()
    {
        Debug.Log("You Win!");
        // Display victory screen
    }
}
