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

public class SceneMover2 : MonoBehaviour
{
  // Method to load a new scene by name
    async public void ChangeScene(string sceneName)
    {

        //string currentSceneName = "";
        string lastSceneMini = PlayerPrefs.GetString("Mini LastScene", "DefaultSceneName");
        //Debug.Log("Last scene was: " );
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "CCMiniGame" ){
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            string playerID = AuthenticationService.Instance.PlayerId;
            //currentSceneName = SceneManager.GetActiveScene().name;

            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");

            //DocumentReference docRef = db.Collection("Users").Document(playerID);
            //Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
            //QuerySnapshot snapshot = await query.GetSnapshotAsync();

            Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                DocumentSnapshot docRef = snapshot.Documents.First();

                //Dictionary<string, object> minisDict = docRef.GetValue<Dictionary<string, object>>("minisFRname");
                List<object> minisList = docRef.GetValue<List<object>>("miniComp");

                if (minisList.Contains(currentSceneName))
                {
                    SceneManager.LoadScene("Backyard");

                } else {
                    DocumentReference docRef2 = snapshot.Documents.First().Reference;
                    Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        { "miniComp", FieldValue.ArrayUnion(currentSceneName) },
                        { "minisFRname", FieldValue.ArrayUnion(lastSceneMini) }
                    };
                    await docRef2.UpdateAsync(updates);
                    Debug.Log($"Updated Firestore for player {playerID} with scene: {currentSceneName}");

                    PlayerPrefs.SetString("LastScene", currentSceneName);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(sceneName);
/*
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
                        */


                    //SceneManager.LoadScene(miniNameIN);
                }
            }
            /*if (snapshot.Count > 0)
            {
                DocumentReference docRef = snapshot.Documents.First().Reference;
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "miniComp", FieldValue.ArrayUnion(currentSceneName) },
                    { "minisFRname", FieldValue.ArrayUnion(lastSceneMini) }
                };
                await docRef.UpdateAsync(updates);
                Debug.Log($"Updated Firestore for player {playerID} with scene: {currentSceneName}");
            }
            PlayerPrefs.SetString("LastScene", currentSceneName);
            PlayerPrefs.Save();
            SceneManager.LoadScene(sceneName);
        } else {
            PlayerPrefs.SetString("LastScene", currentSceneName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Backyard");
        }*/
        }
        //SceneManager.LoadScene("Backyard");
    }
    

}

/*
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
                        */