using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;
    float score = 0;

    [SerializeField]
    bool isGrounded = false; 
    bool isAlive = true;

    Rigidbody2D RB;

    public Text ScoreTxt;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        score = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false; 
            }
        }

        if (isAlive)
        {
            score += Time.deltaTime * 4;
            ScoreTxt.text = "Score:" + score.ToString("F");
        }

        if (isAlive == false)
        {
            SceneManager.LoadScene("losecrabgame");
        }

        if (score >= 100)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("Mini LastScene", currentSceneName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("wincrabgame");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.CompareTag("trash"))
        {
            isAlive = false; 
            Time.timeScale = 0;
        }
    }
}
