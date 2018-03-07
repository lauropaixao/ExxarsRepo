using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class EmailLogin : MonoBehaviour 
{
	public InputField email, password;
	public Text status;

	public void SignIn ()
	{
		FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync (email.text, password.text).ContinueWith ((obj) => 
			{
				status.text = "Login Realizado Com Sucesso";
			});
	}

	public void SignUn ()
	{
		FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync (email.text, password.text).ContinueWith ((obj) => 
			{
				status.text = "Cadastro Realizado Com Sucesso";
			});
	}

}
