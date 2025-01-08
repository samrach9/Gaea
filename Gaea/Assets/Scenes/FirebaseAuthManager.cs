using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth; // Firebase Authentication instance

    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public Button signUpButton; // Button to trigger the sign-up
    public int scenenum; // The scene to load

    void Start()
    {
        // Initialize Firebase Auth
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == DependencyStatus.Available)
            {
                // Initialize Firebase Auth
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError($"Firebase dependencies not resolved: {task.Result}");
            }
        });

        // Add a listener to the sign-up button
        if (signUpButton != null)
        {
            signUpButton.onClick.AddListener(SignUpUser);
            //button.onClick.AddListener(OnButtonPress);
        }
    }

    void SignUpUser()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        // Validate email and password before sending to Firebase
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Email or password is empty.");
            return;
        }

        // Create a new user with Firebase Authentication
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created successfully
            Firebase.Auth.AuthResult result = task.Result;
            SceneManager.LoadScene(scenenum);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }
}
