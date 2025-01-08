using Firebase.Firestore;
using UnityEngine;
using Firebase.Extensions;

public class firebaseSetupTest : MonoBehaviour
{
    FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        // Example: Writing data to Firestore
        /*
        DocumentReference docRef = db.Collection("TestCollection").Document("TestDocument");
        docRef.SetAsync(new { Name = "Unity Editor", Timestamp = Timestamp.GetCurrentTimestamp() })
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data successfully written to Firestore!");
                }
                else
                {
                    Debug.LogError("Failed to write data: " + task.Exception);
                }
            });
            */
    }
}



