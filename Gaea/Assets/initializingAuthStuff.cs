using System;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;

public class initializingAuthStuff : MonoBehaviour
{
	async void Start()
    {
        // Initialize Unity Services
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully.");

            SetupEvents(); // Set up authentication events
            await SignInPlayer(); // Attempt to sign in the player
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to initialize Unity Services: {ex.Message}");
        }
    } 
    // Setup authentication event handlers if desired
    void SetupEvents() {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
            {
                Debug.Log("Player session could not be refreshed and expired.");
            };
    }
    async System.Threading.Tasks.Task SignInPlayer()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Sign-in failed: {ex.Message}");
        }
    }
}
