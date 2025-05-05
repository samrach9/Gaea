using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToBeach : MonoBehaviour
{
    public void ToBeach()
    {
        SceneManager.LoadScene("Beach");
    }
}
