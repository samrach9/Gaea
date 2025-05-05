using UnityEngine.SceneManagement;
using UnityEngine;

public class TakeToBambooGame : MonoBehaviour
{
    public void GoToBamboo()
    {
        SceneManager.LoadScene("BambooIntro");
    }
}
