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

public class movetoCARBON : MonoBehaviour
{
    public void clickCCfromaLEVEL(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("whereIclickedONCC", currentSceneName);
        PlayerPrefs.Save();
        SceneManager.LoadScene("CarbonCreditsHome");
    }
}
