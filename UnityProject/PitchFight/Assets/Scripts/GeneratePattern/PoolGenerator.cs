//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

//******************************************************************************

public enum ObjectType
{
	OBSTACLE = 0,
	STORE,
	GROUND,
	EVENT
}

public class ObjectStack
{
	public ObjectType Type;
	public Stack<GameObject> Objects;
}

public class PoolGenerator : MonoBehaviour 
{
#region Script Parameters
#endregion

#region Static
#endregion

#region Properties
	private static PoolGenerator mInstance;
	public static PoolGenerator Get {get {return mInstance;}}
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private const int DEFAULT_DUPLICATION = 6;

	// Private -----------------------------------------------------------------
	private int mMaxEnumType;
	private List<ObjectStack> mPool;
#endregion

#region Unity Methods
	void Awake()
	{
		if (mInstance != null)
		{
			Debug.LogWarning("PoolGenerator - we were instantiating a second copy of PoolGenerator, so destroying this instance");
			DestroyImmediate(this.gameObject, true);
			return;
		}
		mInstance = this;
		DontDestroyOnLoad(this);
		Initialization();
	}

	void Start () 
	{
	}
	
	void Update () 
	{
	}
#endregion

#region Methods
	public GameObject GetRandomObject(ObjectType type)
	{
		return null;
	}
	public GameObject GetObject(ObjectType type, string name)
	{
		return null;
	}
#endregion

#region Implementation

	private void Initialization()
	{
		mMaxEnumType = (int)Enum.GetValues(typeof(ObjectType)).Cast<ObjectType>().Last();


		InitPoolGenerator();
	}
	private void InitPoolGenerator()
	{
		for (int i = 0; i < mMaxEnumType; i++)
		{

		}
	}
	private void GeneratePool(ObjectType type)
	{

	}
#endregion
}
