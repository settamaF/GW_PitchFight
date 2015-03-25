//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************

public enum SoundEvent
{
	HIT,
	DIED,
	VICTORY,
	JUMP,
	HALLELUJAH,
	PO_UP,
	COLLECTIBLES,
	VOICE_M,
	VOICE_F,
	AMB_CITY_LOOP
}

[System.Serializable]
public class Sound
{
	public SoundEvent	NameEvent;
	public string		SoundName;
}

public class SoundManager : MonoBehaviour
{
#region Script Parameters
	public List<Sound> SoundList;
#endregion

#region Properties
	private static SoundManager mInstance;
	public static SoundManager Get { get { return mInstance; } }
#endregion

#region Unity Methods
	void Awake ()
	{
		if (mInstance != null && mInstance != this)
		{
			Debug.LogWarning("SoundManager - we were instantiating a second copy of SoundManager, so destroying this instance");
			DestroyImmediate(this.gameObject, true);
			return;
		}
		DontDestroyOnLoad(this);
		mInstance = this;
	}
#endregion

#region Methods
	public void Play(SoundEvent soundEvent)
	{
		string name = "";

		foreach (var sound in SoundList)
		{
			if (sound.NameEvent == soundEvent)
			{
				name = sound.SoundName;
				break;
			}
		}
		if (!string.IsNullOrEmpty(name))
		{
			AkSoundEngine.PostEvent(name, gameObject);
			return;
		}
		else
			Debug.LogError("Error no sound defined for event: " + soundEvent);
	}
#endregion

#region Implementation
#endregion
}
