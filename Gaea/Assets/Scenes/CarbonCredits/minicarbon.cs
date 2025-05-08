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

public class minicarbon : MonoBehaviour
{
    // Start is called before the first frame update

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
    // Update is called once per frame
    async System.Threading.Tasks.Task updateCredits()
    {
        string playerID = AuthenticationService.Instance.PlayerId;
        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference miniRef = db.Collection("MiniCC").Document("Counts");

        //int scorePlus=0;
        DocumentSnapshot miniSnapshot = await miniRef.GetSnapshotAsync();

        string lastScene = PlayerPrefs.GetString("LastScene", "DefaultSceneName");
        Debug.Log("Last scene was: " + lastScene);

        int scorePlus = miniSnapshot.GetValue<int>(lastScene);
        Debug.Log("scoplus is: " + lastScene);
        

        Query query = db.Collection("Users").WhereEqualTo("pID", playerID);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        if (snapshot.Count > 0)
            {
                DocumentSnapshot userDoc = snapshot.Documents.First();
                DocumentReference docRef = userDoc.Reference;

                int currentCredits = 0;
                if (userDoc.ContainsField("CarbonCredits"))
                {
                    currentCredits = userDoc.GetValue<int>("CarbonCredits");
                }

                int updatedCredits = currentCredits + scorePlus;

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { "CarbonCredits", updatedCredits }
                };

                await docRef.UpdateAsync(updates);

                CreditNum.text = scorePlus.ToString();
                Debug.Log("Updated CarbonCredits to: " + updatedCredits);
            }

    }
}
