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

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
	private SoundManager mSoundManager;
#endregion

#region Unity Methods
	void Start ()
	{
		mSoundManager = SoundManager.Get;
		if (!mSoundManager)
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
			mSoundManager.Play(SoundEventDebug);
		}
	}
#endregion
}
