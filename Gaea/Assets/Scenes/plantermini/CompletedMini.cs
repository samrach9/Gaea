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

public class CompletedMini : MonoBehaviour
{
    // Start is called before the first frame update
    public string miniNameIN;

    async public void ChangeScene(){

            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            string playerID = AuthenticationService.Instance.PlayerId;
            
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");

            //DocumentReference docRef = db.Collection("Users").Document(playerID);
            Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                DocumentSnapshot docRef = snapshot.Documents.First();

                //Dictionary<string, object> minisDict = docRef.GetValue<Dictionary<string, object>>("minisFRname");
                List<object> minisList = docRef.GetValue<List<object>>("minisFRname");

                if (minisList.Contains(miniNameIN))
                {
                    SceneManager.LoadScene("endPM");
                } else {
                    SceneManager.LoadScene(miniNameIN);
                }
            }
    }
}
