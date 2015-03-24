//******************************************************************************
// Author: Frédéric SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections.Generic;

public enum FX
{
	FX1,
	FX2,
	FX3
}

public class FxManager : MonoBehaviour
{
	[System.Serializable]
	public class FxEvent
	{
		public FX			EventName;
		public GameObject	FxPrefab;
	}

#region Static
	private static FxManager mInstance;
	public static FxManager Get { get{ return mInstance; } }
#endregion
	
#region Script Parameters
	public List<FxEvent>	FX;
#endregion
	
#region Unity Methods
	void Awake()
	{
		if(mInstance != null)
		{
			UnityEngine.Debug.Log("FxManager - we were instantiating a second copy of FxManager, so destroying this instance");
			DestroyImmediate(this.gameObject, true);
			return;
		}
		
		// Keep this object alive for the duration of the game
		DontDestroyOnLoad(this);
		mInstance = this;
	}
#endregion
	
#region Methods
	public void Play(FX fx, Transform target)
	{
		GameObject instance;
		var gameObject = GetPrefab(fx);
		
		if (!gameObject)
		{
			Debug.LogWarning("Not prefab " + fx.ToString() + " defined");
			return;
		}
		instance = Instantiate(gameObject) as GameObject;
		instance.transform.parent = target;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localRotation = Quaternion.identity;
		instance.SetActive(true);
	}

	public void Play(FX fx, Component target)
	{
		Play(fx, target.transform);
	}

	public void Play(FX fx, Vector3 position, Quaternion rotation)
	{
		GameObject instance;
		var gameObject = GetPrefab(fx);

		if (!gameObject)
		{
			Debug.LogWarning("Not prefab " + fx.ToString() + " defined");
			return;
		}
		instance = Instantiate(gameObject) as GameObject;
		instance.transform.parent = transform.parent;
		instance.transform.localPosition = position;
		instance.transform.localRotation = rotation;
		instance.SetActive(true);
	}
#endregion

#region Implementation
	private GameObject GetPrefab(FX fx)
	{
		foreach (var prefab in FX)
		{
			if (fx == prefab.EventName)
				return prefab.FxPrefab;
		}
		return null;
	}
#endregion
}
