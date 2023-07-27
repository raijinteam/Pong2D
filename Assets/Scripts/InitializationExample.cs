

using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using System.Collections.Generic;

public class InitializationExample : MonoBehaviour
{

	public static InitializationExample instance;

    private void Awake()
    {
		instance = this;
    }


	[SerializeField] private SaveData data;

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log(UnityServices.State);


		SetupEvents();

		

	}



	public async void SaveDataInCloud(string key , string value)
    {
        try
        {
			var data = new Dictionary<string, object> { { key, value } };
			Debug.Log("Data is saved");
			await CloudSaveService.Instance.Data.ForceSaveAsync(data);
		}catch(CloudSaveException ce)
        {
			Debug.Log("Not Saved : " + ce);
        }
    }


    // Setup authentication event handlers if desired
    void SetupEvents()
	{
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


	private async Task AnonymousLogin()
    {
        try
        {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();

			Debug.Log("Anonymous sign in success");

			Debug.Log($"Player Id : " + AuthenticationService.Instance.PlayerId);
		}catch(AuthenticationException ax)
        {
			//show player has proper message not sign up or login and why
			Debug.Log(ax);
        }catch(RequestFailedException re){
			Debug.Log(" Request Faild : " + re );
        }
    }



	public async void OnClick_LoginAsAGuast()
    {
		await AnonymousLogin();
		data.gameObject.SetActive(true);
	}
}
