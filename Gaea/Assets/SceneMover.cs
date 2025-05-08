using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
  // Method to load a new scene by name
    public void ChangeScene(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
    }

    public void ChangeSceneString(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}
