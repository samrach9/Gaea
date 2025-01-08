using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class loginSceneScript : MonoBehaviour
{
    public Text statusText;
    
    async void Start()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log($"Player signed in: {AuthenticationService.Instance.PlayerId}");
            };

            AuthenticationService.Instance.SignedOut += () =>
            {
                Debug.Log("Player signed out");
            };

            AuthenticationService.Instance.SignInFailed += (error) =>
            {
                Debug.LogError($"Sign-in failed: {error}");
            };
        }
    }
    public void UpdateStatus()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            statusText.text = $"Signed in as: {AuthenticationService.Instance.PlayerId}";
        }
        else
        {
            statusText.text = "Not signed in";
        }
    }
    public void SignIn()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError($"Failed to sign in: {task.Exception}");
                }
                else
                {
                    Debug.Log("Signed in anonymously!");
                }
            });
        }
    }
    public void SignOut()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignOut();
            Debug.Log("Signed out.");
        }
    }
}