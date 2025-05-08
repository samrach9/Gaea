using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BlvlSel : MonoBehaviour
{
    public Button mooovie;
    async void Start()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        mooovie.onClick.AddListener(OnBeachButtonClicked);
    }
    public async void OnBeachButtonClicked()
    {/*

        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
        CollectionReference usersRef = db.Collection("Users");
        Query query = usersRef.WhereEqualTo("pID", AuthenticationService.Instance.PlayerId);

        QuerySnapshot snapshot = await query.GetSnapshotAsync();
        if (snapshot.Count > 0)
        {
            //DocumentSnapshot userDoc = snapshot.Documents[0];
            DocumentReference docRef = userDoc.Reference;

            // Update the 'level' field to "beach"
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "level", "beach" }
            };
            await docRef.UpdateAsync(updates);
        }
        SceneManager.LoadScene("Beach");*/
    }
}