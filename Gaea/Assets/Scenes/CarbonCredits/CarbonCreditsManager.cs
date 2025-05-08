using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CarbonCreditsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Streak;
    public TextMeshProUGUI CreditNum;

    public Button mooovie;

    async void Start()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        updateCredits();
    }
    public void updateCredits()
    {
        //string Carbos = "";
        //string pissy = "";
        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
        CollectionReference usersRef = db.Collection("Users");
        Debug.Log("Running Firestore query for ID = " + AuthenticationService.Instance.PlayerId);
        Query query = usersRef.WhereEqualTo("pID", AuthenticationService.Instance.PlayerId);
        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    QuerySnapshot snapshot = task.Result;
                    if (snapshot.Count > 0)
                    {
                        foreach (DocumentSnapshot document in snapshot.Documents) {
                
                        Dictionary<string, object> userData = document.ToDictionary();
                        
                        string carbos = userData["CarbonCredits"].ToString();
                        //Carbos = userData["carbonCredits"].ToString();
                        string pissfire = userData["StreakCount"].ToString();
                        //pissy = userData["StreakCount"].ToString();
                        Debug.Log("Retrieved carbon credits: " + carbos);
                        Debug.Log("Retrieved streak count: " + pissfire);
                        Streak.text = $"{pissfire}";
                        CreditNum.text = $"{carbos}";
                        }
                    }
                    else
                    {
                        Debug.Log("No such user found in Firestore.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to get document: " + task.Exception);
                }
            });
    }
    //public void buttonnsn(){
    //    SceneManager.LoadScene("Backyard");
    //}
}
