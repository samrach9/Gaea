using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToBambooMiniGame : MonoBehaviour
{
    public void GoToBambooMini()
    {
        Debug.Log("iwas slcicked");
        SceneManager.LoadScene("BambooMiniGame");
    }
}
