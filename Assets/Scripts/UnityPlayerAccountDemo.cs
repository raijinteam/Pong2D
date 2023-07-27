using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.PlayerAccounts;
using UnityEngine;

public class UnityPlayerAccountDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityServices.InitializeAsync();

        Debug.Log("Token : " +  PlayerAccountService.Instance.AccessToken);
        SignInWithUnity();
        SetupEvents();
        
    }

    // Setup authentication event handlers if desired
    void SetupEvents()
    {
        Debug.Log("Event setup");

        PlayerAccountService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"Access Token: {PlayerAccountService.Instance.AccessToken}");

            // Shows how to get an access token
            //Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        PlayerAccountService.Instance.SignInFailed += (err) => {
            Debug.LogError("Sign in faild : " + err);
        };

        PlayerAccountService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

       
    }


    async void SignInWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
            Debug.Log("SignIn is successful create new account");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }


    private async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task LinkWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithUnityAsync(accessToken);
            Debug.Log("Link is successful.");
        }
        catch (AuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            // Prompt the player with an error message.
            Debug.LogError("This user is already linked with another account. Log in instead.");
        }

        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
