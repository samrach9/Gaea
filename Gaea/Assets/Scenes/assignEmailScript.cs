


using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro; // For TextMeshPro Input Field
using Unity.Services.Authentication;
using Unity.Services.Core;
using Firebase.Firestore;
using Firebase.Extensions;

public class assignEmailScript : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField emailInputField; 
    public TMP_InputField passwordInputField; 
    public Button setNameButton; // Reference to the Button

    async void Start()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Signed in with Player ID: {AuthenticationService.Instance.PlayerId}");
        }
    }

    public async void SetPlayerName()
    {
        string playerName = nameInputField.text; // Get the input text
        string playerEmail = emailInputField.text;
        string playerPassword = passwordInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty.");
            return;
        }
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            Debug.Log($"Player name updated successfully to: {playerName}");

            FirebaseFirestore db;
            
            db = FirebaseFirestore.DefaultInstance;

            // Example: Writing data to Firestore
            DocumentReference docRef = db.Collection("Users").Document(playerName);
            await docRef.SetAsync(new { ID = AuthenticationService.Instance.PlayerId, Name = playerName, Email = playerEmail, StreakCount = 1, password = playerPassword, scene = "backyard", carbonCredits = 0, Timestamp = Timestamp.GetCurrentTimestamp() })
            
            //DocumentReference docRef = db.Collection("Users").Document(playerName);
            //await docRef.SetAsync(new { Name = playerName, Email = playerEmail, password = playerPassword, Timestamp = Timestamp.GetCurrentTimestamp() })
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


        }
        catch (System.Exception ex)
        {
            //statusText.text = "Failed to update name. Please try again.";
            Debug.LogError($"Error updating player name: {ex.Message}");
        }
    }
}