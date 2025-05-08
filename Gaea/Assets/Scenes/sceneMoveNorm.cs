using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMoveNorm : MonoBehaviour
{
    // Start is called before the first frame update
    async public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
