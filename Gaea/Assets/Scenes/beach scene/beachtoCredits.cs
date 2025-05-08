using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class beachtoCredits : MonoBehaviour
{
    public void clickMe(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "CarbonCreditsHome"){
            string lastSceneMini = PlayerPrefs.GetString("Mini LastScene", "DefaultSceneName");
            SceneManager.LoadScene(lastSceneMini);
        } else {
            PlayerPrefs.SetString("Mini LastScene", currentSceneName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("CarbonCreditsHome");
        }
    }
}
