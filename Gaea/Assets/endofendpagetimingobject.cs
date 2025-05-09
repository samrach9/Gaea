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

public class endofendpagetimingobject : MonoBehaviour
{
    async public void arran(){
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
                    Debug.Log("added end of endpage timing");
                }
    }
}
