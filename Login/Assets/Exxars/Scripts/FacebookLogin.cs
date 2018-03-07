using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Linq;
using Firebase;
using Firebase.Auth;

internal class FacebookLogin : MonoBehaviour
{
	public void SignIn ()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
		print ("Esse é o Token do Cliente_01: " + Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
	}

	private void AuthCallback (ILoginResult result) 
	{
		if (FB.IsLoggedIn)
		{
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions)
			{
				Debug.Log(perm);
			}
			print ("Esse é o Token do Cliente_02: " + Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
			Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
			FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(task => {
				if (task.IsCanceled) 
				{
					Debug.LogError("SignInWithCredentialAsync was canceled.");
					return;
				}
				if (task.IsFaulted)
				{
					Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
					return;
				}
				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
			});
		} 
		else
		{
			Debug.Log("User cancelled login");
		}
	}



}
