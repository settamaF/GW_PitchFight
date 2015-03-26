//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class ActiveEvent : MonoBehaviour 
{
	public SoundEvent	EnableSound = SoundEvent.NOTHING;
	public SoundEvent	DisableSound = SoundEvent.NOTHING;
	public string		GroupeState;
	public string		StateEnable;
	public string		StateDisable;

	private bool mActive;
#region Unity Methods
	void OnDisable()
	{
		if (string.IsNullOrEmpty(GroupeState))
			SoundManager.Get.Play(DisableSound);
		else
			SoundManager.Get.SetState(GroupeState, StateDisable);
	}

	void OnEnable()
	{
		mActive = true;
	}

	void Update()
	{
		if (mActive)
		{
			mActive = false;
			if (string.IsNullOrEmpty(GroupeState))
				SoundManager.Get.Play(EnableSound);
			else
			{
				if (EnableSound != SoundEvent.NOTHING)
					SoundManager.Get.Play(EnableSound);
				SoundManager.Get.SetState(GroupeState, StateEnable);
			}
		}
	}
#endregion
}
