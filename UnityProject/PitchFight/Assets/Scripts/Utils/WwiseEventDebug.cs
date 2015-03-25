//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class WwiseEventDebug : MonoBehaviour 
{
#region Script Parameters
	public SoundEvent	SoundEventDebug;
	public KeyCode		KeyInputDebug = KeyCode.Space;
#endregion

#region Unity Methods
	void Start ()
	{
		if (!SoundManager.Get)
		{
			Debug.LogError("No sound Manager in the scene");
			DestroyImmediate(this);
		}
	}

	void Update ()
	{
		if (Input.GetKeyUp(KeyInputDebug))
		{
			Debug.Log("the sound event " + SoundEventDebug + " is playing");
			SoundManager.Get.Play(SoundEventDebug);
		}
	}
#endregion
}
