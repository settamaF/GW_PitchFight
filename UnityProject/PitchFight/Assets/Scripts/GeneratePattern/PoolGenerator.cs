//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;
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

[System.Serializable]
public class ObjectStack
{
	public ObjectType			Type;
	public string				Name;
	public Stack<GameObject>	Objects;
}

[System.Serializable]
public class ObjectPrefab
{
	public ObjectType Type;
	public List<GameObject> Prefabs;
}

public class PoolGenerator : MonoBehaviour 
{
#region Script Parameters
	public List<ObjectPrefab> ObjectPrefabs;
#endregion

#region Properties
	private static PoolGenerator mInstance;
	public static PoolGenerator Get {get {return mInstance;}}
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private const int	DEFAULT_DUPLICATION = 6;
	private const int	OFFSETX = -10000;
	private const int	OFFSETY = -10000;

	// Private -----------------------------------------------------------------
	private int mMaxEnumType;
	public List<ObjectStack> mPool; // a mettre en private
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
#endregion

#region Methods
	public GameObject GetRandomObject(ObjectType type)
	{
		int rand = Random.Range(0, 1);
		return null;
	}
	public GameObject GetObject(ObjectType type, string name)
	{
		GameObject ret;


		return null;
	}


#endregion

#region Implementation

	private void Initialization()
	{
		mMaxEnumType = (int)System.Enum.GetValues(typeof(ObjectType)).Cast<ObjectType>().Last();
		this.transform.position = new Vector3(OFFSETX, OFFSETY, 0);
		InitPoolGenerator();
	}
	private void InitPoolGenerator()
	{
		mPool = new List<ObjectStack>();
		for (int i = 0; i < mMaxEnumType; i++)
		{
			int index = GetIndexPrefab((ObjectType)i);

			if (index >= 0)
			{
				foreach (var prefab in ObjectPrefabs[index].Prefabs)
				{
					ObjectStack stack = new ObjectStack();
					stack.Type = (ObjectType)i;
					stack.Name = prefab.name;
					stack.Objects = GeneratePool(prefab);
					mPool.Add(stack);
				}
			}
		}
	}

	private int GetIndexPrefab(ObjectType type)
	{
		for (int i = 0; i < ObjectPrefabs.Count; i++)
		{
			if (ObjectPrefabs[i].Type == type)
				return i;
		}
		return -1;
	}

	private int GetIndexStack(ObjectType type)
	{
		for (int i = 0; i < mPool.Count; i++)
		{
			if (mPool[i].Type == type)
				return i;
		}
		return -1;
	}
	private Stack<GameObject> GeneratePool(GameObject prefab)
	{
		Stack<GameObject> ret = new Stack<GameObject>();

		for (int i = 0; i < DEFAULT_DUPLICATION; i++)
		{
			GameObject obj = InstantiatePrefab(prefab);

			if (obj)
			{
				ret.Push(obj);
			}
			else
			{
				Debug.LogError("Error instantiate prefab: " + prefab.name);
				break;
			}
		}
		return ret;
	}

	private GameObject InstantiatePrefab(GameObject prefab)
	{
		GameObject ret;

		ret = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		ret.transform.parent = this.transform;
		return ret;
	}
#endregion
}
