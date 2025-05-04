using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class BugManager : MonoBehaviour
{
    public GameObject[] bugObjects; 
    public TMP_Text      scoreText;
    public Vector2       minSpawnPos = new Vector2(-8f, -4f);
    public Vector2       maxSpawnPos = new Vector2(8f,  4f);

    private int          score = 0;

    // We’ll keep track of our two current bugs and whether each was clicked:
    private GameObject   bugA, bugB;
    private bool         bugAClicked, bugBClicked;

    void Start()
    {
        UpdateScoreText();
        // hide & hook up each bug prefab once
        foreach (var bug in bugObjects)
        {
            var sr = bug.GetComponent<SpriteRenderer>();
            sr.enabled = false;

            bool isBad = bug.name.ToLower().Contains("bad");
            var handler = bug.AddComponent<BugClickHandler>();
            handler.Init(this, isBad, bug);
        }

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            // reset click flags
            bugAClicked = bugBClicked = false;

            if (score >= 60)
                ShowTwoBugs();
            else
                ShowOneBug();

            // now run both lifetimes _in parallel_
            float lifeA = bugA.name.ToLower().Contains("bad") ? 1.2f : 2f;
            float lifeB = bugB != null && bugB.name.ToLower().Contains("bad")
                          ? 1.2f : 2f;

            var waitA = StartCoroutine(BugLifetime(bugA, lifeA, () => !bugAClicked));
            Coroutine waitB = null;
            if (bugB != null)
                waitB = StartCoroutine(BugLifetime(bugB, lifeB, () => !bugBClicked));

            // wait for both
            yield return waitA;
            if (waitB != null) yield return waitB;

            UpdateScoreText();
            if (score>=120){
                Debug.Log("JUST WON!!! GAME");
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("endPM");
                yield break;
            }
        }
    }

    void ShowOneBug()
    {
        HideIf(bugA);
        bugB = null;

        bugA = PickRandomBug();
        PositionAndShow(bugA);
    }

    void ShowTwoBugs()
    {
        HideIf(bugA);
        HideIf(bugB);

        bugA = PickRandomBug();
        bugB = PickRandomBug();

        PositionAndShow(bugA);
        PositionAndShow(bugB);
    }

    IEnumerator BugLifetime(GameObject bug, float duration, System.Func<bool> wasNotClicked)
    {
        yield return new WaitForSeconds(duration);
        if (bug != null && wasNotClicked())
        {
            // only penalize/reward if user never clicked it
            bool isBad = bug.name.ToLower().Contains("bad");
            score += isBad ? -10 : +5;
        }

        HideIf(bug);
    }

    GameObject PickRandomBug()
    {
        int idx = Random.Range(0, bugObjects.Length);
        return bugObjects[idx];
    }

    void PositionAndShow(GameObject bug)
    {
        bug.transform.position = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f
        );
        bug.GetComponent<SpriteRenderer>().enabled = true;
    }

    void HideIf(GameObject bug)
    {
        if (bug != null)
            bug.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Called by BugClickHandler with the exact bug that was clicked
    public void BugClicked(GameObject bug, bool isBad)
    {
        // update their individual clicked flag
        if (bug == bugA) bugAClicked = true;
        else if (bug == bugB) bugBClicked = true;
        else return; // stray click

        // score
        score += isBad ? +10 : -5;
        HideIf(bug);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}





/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class bugmanager : MonoBehaviour
{
    public GameObject[] bugObjects; // Drag your bug GameObjects here (already in scene)
    public TMP_Text scoreText;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;

    private GameObject currentBug;
    private GameObject currentBug2;
    private bool bugIsActive = false;
    private bool bugClicked = false;

    void Start()
    {
        // Hide all bugs at the start
        UpdateScoreText();
        foreach (var bug in bugObjects)
        {
            bug.GetComponent<SpriteRenderer>().enabled = false;

            // Attach handler once (and determine good/bad)
            bool isBad = bug.name.ToLower().Contains("bad");
            bug.AddComponent<BugClickHandler>().Init(this, isBad);
        }

        StartCoroutine(ActivateBugLoop());
    }

    IEnumerator ActivateBugLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));  // random delay before showing the next bug

            // Check if score is >= 65, and spawn two bugs
            if (score >= 60)
            {
                ShowTwoBugs();
            }
            else
            {
                ShowRandomBug();
            }

            // Set the bug's lifetime based on whether it's good or bad
            float bugLifetime = currentBug.name.ToLower().Contains("bad") ? 1.2f : 2.0f;
            float bugLifetime2 = currentBug2 != null && currentBug2.name.ToLower().Contains("bad") ? 1.2f : 2.0f;

            // Wait for the bug(s) to expire based on their type
            yield return new WaitForSeconds(bugLifetime);
            if (currentBug2 != null)
            {
                yield return new WaitForSeconds(bugLifetime2);
            }

            if (!bugClicked) // Check if the bug wasn't clicked
            {
                // If the bug was not clicked, adjust score based on type
                AdjustScoreBasedOnBugType(currentBug);
                if (currentBug2 != null)
                {
                    AdjustScoreBasedOnBugType(currentBug2);
                }
            }

            HideBug();
            if (currentBug2 != null) HideSecondBug();
            bugClicked = false; // Reset the bugClicked flag for the next cycle
            UpdateScoreText();
        }
    }

    void ShowRandomBug()
    {
        HideBug(); // hide previous one

        int index = Random.Range(0, bugObjects.Length);
        currentBug = bugObjects[index];

        Vector3 randomPos = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );

        currentBug.transform.position = randomPos;
        currentBug.GetComponent<SpriteRenderer>().enabled = true;
        bugIsActive = true;
    }

    void ShowTwoBugs()
    {
        HideBug(); // Hide previous bug

        int index1 = Random.Range(0, bugObjects.Length);
        int index2 = Random.Range(0, bugObjects.Length);

        currentBug = bugObjects[index1];
        currentBug2 = bugObjects[index2];

        // Position for two bugs
        Vector3 randomPos1 = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );
        Vector3 randomPos2 = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );

        // Set both bugs' positions and enable them
        currentBug.transform.position = randomPos1;
        currentBug.GetComponent<SpriteRenderer>().enabled = true;

        currentBug2.transform.position = randomPos2;
        currentBug2.GetComponent<SpriteRenderer>().enabled = true;

        bugIsActive = true;
    }

    void HideBug()
    {
        if (currentBug != null)
        {
            currentBug.GetComponent<SpriteRenderer>().enabled = false;
        }
        bugIsActive = false;
    }

    void HideSecondBug()
    {
        if (currentBug2 != null)
        {
            currentBug2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void AdjustScoreBasedOnBugType(GameObject bug)
    {
        if (bug != null)
        {
            bool isBadBug = bug.name.ToLower().Contains("bad");
            if (isBadBug)
            {
                score -= 10; // Decrease score for bad bugs not clicked
            }
            else
            {
                score += 5; // Increase score for good bugs not clicked
            }
        }
    }

    public void BugClicked(bool isBad)
    {
        if (!bugIsActive) return;

        if (isBad) score += 10;
        else score -= 5;
        Debug.Log("score is: " + score);

        HideBug();
        bugClicked = true; // Mark that the bug was clicked
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class bugmanager : MonoBehaviour
{
    public GameObject[] bugObjects; // Drag your bug GameObjects here (already in scene)
    public TMP_Text scoreText;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;

    private GameObject currentBug;
    private bool bugIsActive = false;
    private bool bugClicked = false;

    void Start()
    {
        // Hide all bugs at the start
        UpdateScoreText();
        foreach (var bug in bugObjects)
        {
            bug.GetComponent<SpriteRenderer>().enabled = false;

            // Attach handler once (and determine good/bad)
            bool isBad = bug.name.ToLower().Contains("bad");
            bug.AddComponent<BugClickHandler>().Init(this, isBad);
        }

        StartCoroutine(ActivateBugLoop());
    }

    IEnumerator ActivateBugLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));  // random delay before showing the next bug
            ShowRandomBug();

            // Set the bug's lifetime based on whether it's good or bad
            float bugLifetime = currentBug.name.ToLower().Contains("bad") ? 1.2f : 2.0f;

            // Wait for the bug to expire based on its type
            yield return new WaitForSeconds(bugLifetime);

            if (!bugClicked) // Check if the bug wasn't clicked
            {
                // If the bug was not clicked, adjust score based on type
                if (currentBug != null)
                {
                    bool isBadBug = currentBug.name.ToLower().Contains("bad");
                    if (isBadBug)
                    {
                        score -= 10; // Decrease score for bad bugs not clicked
                    }
                    else
                    {
                        score += 5; // Increase score for good bugs not clicked
                    }
                }
            }

            HideBug();
            bugClicked = false; // Reset the bugClicked flag for the next cycle
            UpdateScoreText();
        }
    }

    void ShowRandomBug()
    {
        HideBug(); // hide previous one

        int index = Random.Range(0, bugObjects.Length);
        currentBug = bugObjects[index];

        Vector3 randomPos = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );

        currentBug.transform.position = randomPos;
        currentBug.GetComponent<SpriteRenderer>().enabled = true;
        bugIsActive = true;
    }

    void HideBug()
    {
        if (currentBug != null)
        {
            currentBug.GetComponent<SpriteRenderer>().enabled = false;
        }
        bugIsActive = false;
    }

    public void BugClicked(bool isBad)
    {
        if (!bugIsActive) return;

        if (isBad) score += 10;
        else score -= 5;
        Debug.Log("score is: " + score);

        HideBug();
        bugClicked = true; // Mark that the bug was clicked
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
}*/

/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class bugmanager : MonoBehaviour
{
    public GameObject[] bugObjects; // Drag your bug GameObjects here (already in scene)
    public float spawnInterval = 1.5f;
    public float bugLifetime = 1.2f;
    public TMP_Text scoreText;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;

    private GameObject currentBug;
    private bool bugIsActive = false;
    private bool bugClicked = false;

    void Start()
    {
        // Hide all bugs at the start
        UpdateScoreText();
        foreach (var bug in bugObjects)
        {
            bug.GetComponent<SpriteRenderer>().enabled = false;

            // Attach handler once (and determine good/bad)
            bool isBad = bug.name.ToLower().Contains("bad");
            bug.AddComponent<BugClickHandler>().Init(this, isBad);
        }

        StartCoroutine(ActivateBugLoop());
    }

    IEnumerator ActivateBugLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            ShowRandomBug();

            // Wait for the bug to expire
            yield return new WaitForSeconds(bugLifetime);

            if (!bugClicked) // Check if the bug wasn't clicked
            {
                // If the bug was not clicked, adjust score based on type
                if (currentBug != null)
                {
                    bool isBadBug = currentBug.name.ToLower().Contains("bad");
                    if (isBadBug)
                    {
                        score -= 10; // Decrease score for bad bugs not clicked
                    }
                    else
                    {
                        score += 5; // Increase score for good bugs not clicked
                    }
                }
            }

            HideBug();
            bugClicked = false; // Reset the bugClicked flag for the next cycle
            UpdateScoreText();
        }
    }

    void ShowRandomBug()
    {
        HideBug(); // hide previous one

        int index = Random.Range(0, bugObjects.Length);
        currentBug = bugObjects[index];

        Vector3 randomPos = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );

        currentBug.transform.position = randomPos;
        currentBug.GetComponent<SpriteRenderer>().enabled = true;
        bugIsActive = true;
    }

    void HideBug()
    {
        if (currentBug != null)
        {
            currentBug.GetComponent<SpriteRenderer>().enabled = false;
        }
        bugIsActive = false;
    }

    public void BugClicked(bool isBad)
    {
        if (!bugIsActive) return;

        if (isBad) score += 10;
        else score -= 5;
        Debug.Log("score is: " + score);

        HideBug();
        bugClicked = true; // Mark that the bug was clicked
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class bugmanager : MonoBehaviour
{
    public GameObject[] bugObjects; // Drag your bug GameObjects here (already in scene)
    public float spawnInterval = 1.5f;
    public float bugLifetime = 1.2f;
    public TMP_Text scoreText;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;
    // public Text scoreText;

    private GameObject currentBug;
    private bool bugIsActive = false;

    void Start()
    {
        // Hide all bugs at the start
        UpdateScoreText();
        foreach (var bug in bugObjects)
        {
            bug.GetComponent<SpriteRenderer>().enabled = false;

            // Attach handler once (and determine good/bad)
            bool isBad = bug.name.ToLower().Contains("bad");
            bug.AddComponent<BugClickHandler>().Init(this, isBad);
        }

        StartCoroutine(ActivateBugLoop());
    }

    IEnumerator ActivateBugLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            ShowRandomBug();
            yield return new WaitForSeconds(bugLifetime);
            HideBug();
        }
    }

    void ShowRandomBug()
    {
        HideBug(); // hide previous one

        int index = Random.Range(0, bugObjects.Length);
        currentBug = bugObjects[index];

        Vector3 randomPos = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y),
            1f // explicitly set Z to 1
        );

        currentBug.transform.position = randomPos;
        currentBug.GetComponent<SpriteRenderer>().enabled = true;
        bugIsActive = true;
    }

    void HideBug()
    {
        if (currentBug != null)
        {
            currentBug.GetComponent<SpriteRenderer>().enabled = false;
        }
        bugIsActive = false;
    }

    public void BugClicked(bool isBad)
    {
        if (!bugIsActive) return;

        if (isBad) score += 10;
        else score -= 5;
        Debug.Log("score is: " + score);

        HideBug();
        UpdateScoreText();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}*/














/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bugmanager : MonoBehaviour
{
    public GameObject[] bugPrefabs; // Assign 4 prefabs here (2 good, 2 bad)
    public float spawnInterval = 1.5f;
    public float bugLifetime = 1.2f;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;
    //public Text scoreText;

    private GameObject currentBug;
    private SpriteRenderer currentRenderer;
    private bool bugIsActive = false;

    void Start()
    {
        SpawnInitialBug();
        StartCoroutine(ActivateBugLoop());
        //UpdateScoreText();
    }

    void SpawnInitialBug()
    {
        int bugIndex = Random.Range(0, bugPrefabs.Length);
        currentBug = Instantiate(bugPrefabs[bugIndex], Vector2.zero, Quaternion.identity);
        currentRenderer = currentBug.GetComponent<SpriteRenderer>();
        currentRenderer.enabled = false;

        // Optionally add collider and a click handler
        currentBug.AddComponent<BoxCollider2D>();
        currentBug.AddComponent<BugClickHandler>().Init(this, bugIndex >= 2); // Assume last 2 are "bad"
    }

    IEnumerator ActivateBugLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            ShowBugAtRandomPosition();
            yield return new WaitForSeconds(bugLifetime);
            HideBug();
        }
    }

    void ShowBugAtRandomPosition()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            Random.Range(minSpawnPos.y, maxSpawnPos.y)
        );

        currentBug.transform.position = randomPos;
        currentRenderer.enabled = true;
        bugIsActive = true;
    }

    void HideBug()
    {
        currentRenderer.enabled = false;
        bugIsActive = false;
    }

    public void BugClicked(bool isBad)
    {
        if (!bugIsActive) return;

        if (isBad)
            score += 10;
        else
            score -= 5;

        HideBug();
        //UpdateScoreText();
    }*/

    /*void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }*/
/*
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
}*/

/*using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class bugmanager : MonoBehaviour
{
    //public List<Transform> spawnPoints;
    public GameObject[] bugPrefabs; // Assign 4 prefabs here (2 good, 2 bad)
    public float spawnInterval = 1.5f;
    public float bugLifetime = 1.2f;

    public Vector2 minSpawnPos = new Vector2(-8f, -4f);
    public Vector2 maxSpawnPos = new Vector2(8f, 4f);

    public int score = 0;
    public Text scoreText;

    void Start()
    {
        StartCoroutine(SpawnBugs());
        UpdateScoreText();
    }

    IEnumerator SpawnBugs()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //int spawnIndex = Random.Range(0, spawnPoints.Count);
            int bugIndex = Random.Range(0, bugPrefabs.Length);

            //GameObject bug = Instantiate(bugPrefabs[bugIndex], spawnPoints[spawnIndex].position, Quaternion.identity);

            Vector2 randomPos = new Vector2(
                Random.Range(minSpawnPos.x, maxSpawnPos.x),
                Random.Range(minSpawnPos.y, maxSpawnPos.y)
            );

            GameObject bug = Instantiate(bugPrefabs[bugIndex], randomPos, Quaternion.identity);

            Destroy(bug, bugLifetime);
        }
    }

    public void BugClicked(bool isBad)
    {
        if (isBad)
        {
            score += 10;
        }
        else
        {
            score -= 5;
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (minSpawnPos + maxSpawnPos) / 2f;
        Vector3 size = maxSpawnPos - minSpawnPos;
        Gizmos.DrawWireCube(center, size);
    }
}
*/