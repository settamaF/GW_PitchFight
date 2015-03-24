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

}

public class Sound
{
	public SoundEvent	NameEvent;
	public GameObject	SoundPrefab;
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
		if (mInstance != this)
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
		GameObject gameObject = null;

		foreach (var sound in SoundList)
		{
			if (sound.NameEvent == soundEvent)
			{
				gameObject = sound.SoundPrefab;
				break;
			}
		}
		if (gameObject)
		{
			var instance = Instantiate(gameObject) as GameObject;
			Destroy(instance);
			//Play the sound
			return;
		}
		else
			Debug.LogError("Error no sound prefab for event: " + soundEvent);
	}
#endregion

#region Implementation
#endregion
}
