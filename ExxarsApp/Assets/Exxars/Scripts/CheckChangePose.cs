using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChangePose : MonoBehaviour 
{
	private GameObject Perso_Chan;
	private Animator anim;
	private AnimatorStateInfo currentState;
	private AnimatorStateInfo previousState;

	// Use this for initialization
	void Start () 
	{
		Perso_Chan = GameObject.Find ("unitychan");
		anim = Perso_Chan.GetComponent<Animator>();
		anim.SetBool("Next",true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Perso_Chan = GameObject.Find ("unitychan");
		anim = Perso_Chan.GetComponent<Animator>();
		currentState = anim.GetCurrentAnimatorStateInfo (0);

		if (anim.GetBool ("Next")) 
		{
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			if (previousState.fullPathHash != currentState.fullPathHash) 
			{
				anim.SetBool ("Next", false);
				previousState = currentState;				
			}
		}

		if (anim.GetBool ("Back")) 
		{
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			if (previousState.fullPathHash != currentState.fullPathHash) 
			{
				anim.SetBool ("Back", false);
				previousState = currentState;				
			}
		}
	}
		
}
