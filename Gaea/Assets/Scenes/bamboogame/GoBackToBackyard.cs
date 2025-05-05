using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoBackToBackyard : MonoBehaviour
{
    public void GoToBackyard()
    {
        SceneManager.LoadScene("Backyard");
    }
}
