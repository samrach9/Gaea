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

public class GoToBambooMiniGame : MonoBehaviour
{
    // Start is called before the first frame update
    public string miniNameIN;
    async void Start(){
        await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            string playerID = AuthenticationService.Instance.PlayerId;

            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");

            Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                DocumentSnapshot docRef = snapshot.Documents.First();

                List<object> cardTimes = docRef.GetValue<List<object>>("CardMiniGameTiming");

                    DocumentReference docRef2 = snapshot.Documents.First().Reference;
                    Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        { "CardMiniGameTiming", FieldValue.ArrayUnion(Timestamp.GetCurrentTimestamp()) }
                    };
                    await docRef2.UpdateAsync(updates);
                    Debug.Log($"added start of intro timing");
                }
            }

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
                List<object> cardTimes = docRef.GetValue<List<object>>("CardMiniGameTiming");

                if (minisList.Contains(miniNameIN))
                {
                    SceneManager.LoadScene("BambooWinInfo");
                } else {
                    DocumentReference docRef2 = snapshot.Documents.First().Reference;
                    Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        { "CardMiniGameTiming", FieldValue.ArrayUnion(Timestamp.GetCurrentTimestamp()) }
                    };
                    await docRef2.UpdateAsync(updates);
                    Debug.Log($"added end of intro / start of mini timing");
                    SceneManager.LoadScene(miniNameIN);
                }
            }
            
    }
}
