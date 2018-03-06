using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePose : MonoBehaviour
{
	private GameObject Perso_Chan;
	private Animator anim;


	public void NextPose()
	{
		Perso_Chan = GameObject.Find ("unitychan");
		anim = Perso_Chan.GetComponent<Animator>();
		anim.SetBool("Next",true);
	}

	public void PreviousPose()
	{
		Perso_Chan = GameObject.Find ("unitychan");
		anim = Perso_Chan.GetComponent<Animator>();
		anim.SetBool("Back",true);
	}
}