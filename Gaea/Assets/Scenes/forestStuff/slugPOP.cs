using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class slugPOP : MonoBehaviour
{
    public GameObject forestLeft;        // The object whose sprite must match
    public Sprite forestSlug;            // The sprite to check for
    public GameObject right;             // The object to change
    public Sprite miniPop;               // The new sprite to set


    public GameObject continueButton; // The UI Button GameObject (set inactive by default)
    public string nextSceneName;      // Scene to load

    private bool conditionMet = false;

    void Start()
    {
        if (continueButton != null)
        {
            continueButton.SetActive(false);
            continueButton.GetComponent<Button>().onClick.AddListener(OnContinueClicked);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !conditionMet)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.name == "slurg")
            {
                SpriteRenderer forestLeftRenderer = forestLeft.GetComponent<SpriteRenderer>();

                if (forestLeftRenderer != null && forestLeftRenderer.sprite == forestSlug)
                {
                    SpriteRenderer rightRenderer = right.GetComponent<SpriteRenderer>();
                    if (rightRenderer != null)
                    {
                        rightRenderer.sprite = miniPop;
                        conditionMet = true;

                        if (continueButton != null)
                        {
                            continueButton.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    void OnContinueClicked()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
