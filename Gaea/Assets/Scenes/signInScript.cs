using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class signInScript : MonoBehaviour
{
    //public TMP_InputField nameInputField;
    public TMP_InputField emailInputField; 
    public TMP_InputField passwordInputField; 
    public Button SignInButton; // Reference to the Button

    async void Start()
    {
        //auth = FirebaseAuth.DefaultInstance;

        // Add listener to sign-in button
        //signInButton.onClick.AddListener(SignInUser);

        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            //Debug.Log($"Signed in with Player ID: {AuthenticationService.Instance.PlayerId}");
        }
    }
    public void SignInUser()
    {
        string emailIN = emailInputField.text;
        string passwordIN = passwordInputField.text;
        
        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
        CollectionReference usersRef = db.Collection("Users");
       // Query query = usersRef.WhereEqualTo("ID", AuthenticationService.Instance.PlayerId);
        Query query = usersRef
            .WhereEqualTo("pID", AuthenticationService.Instance.PlayerId)
            .WhereEqualTo("Email", emailIN)
            .WhereEqualTo("password", passwordIN);
        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    QuerySnapshot snapshot = task.Result;
                    if (snapshot.Count >0)
                    {
                        foreach (DocumentSnapshot document in snapshot.Documents) {
                
                        Dictionary<string, object> userData = document.ToDictionary();
                        
                        string emailFire = userData["Email"].ToString();
                        string passFire = userData["password"].ToString();
                        if (emailIN == emailFire && passwordIN == passFire){
                            string name = userData["Name"].ToString();
                            Debug.Log($"Welcome back, {name}!");
                            SceneManager.LoadScene("Backyard");
                        }
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
}