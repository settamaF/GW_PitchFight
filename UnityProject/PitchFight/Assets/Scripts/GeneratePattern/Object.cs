//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class Object : MonoBehaviour 
{
	public class ObjectChild
	{
		public ObjectType Type;
		public List<Transform> Childs;
	}
#region Script Parameters
	public ObjectType Type;
#endregion

#region Static
#endregion

#region Properties
	public Transform Next { get { return mNext; } }
	private Transform mNext;
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
	private List<ObjectChild> mObjectChild;
#endregion

#region Unity Methods
	void Awake()
	{
		if (Type == ObjectType.GROUND)
		{
			mNext = this.transform.FindChild("Next");
			if (!mNext)
			{
				Debug.LogError("Error no child Next in " + this.name);
				return;
			}
			Initialize();
		}
	}
#endregion

#region Methods
	public void Initialize()
	{
		mObjectChild = new List<ObjectChild>();
		for (int i = 0; i < PoolGenerator.mMaxEnumType; i++)
		{
			ObjectChild objectChild = new ObjectChild();

			objectChild.Type = (ObjectType)i;
			objectChild.Childs = Initialize((ObjectType)i);
			mObjectChild.Add(objectChild);
		}
	}

	public void Use()
	{
		foreach (var objectChild in mObjectChild)
		{
			foreach (Transform child in objectChild.Childs)
			{
				if (child.childCount <= 0)
				{
					GameObject gameObject = PoolGenerator.Get.GetRandomObject(objectChild.Type);
					if (gameObject)
					{
						gameObject.transform.parent = child;
						gameObject.transform.localPosition = Vector3.zero;
					}
				}
			}
		}
	}

	public void Reset()
	{
		foreach (var objectChild in mObjectChild)
		{
			foreach (Transform child in objectChild.Childs)
			{
				if (child.childCount > 0)
				{
					for (int i = 0; i < child.childCount; i++)
					{
						PoolGenerator.Get.AddToStack(child.GetChild(i).gameObject, objectChild.Type);
					}
				}
			}
		}
		PoolGenerator.Get.AddToStack(this.gameObject, Type);
	}
#endregion

#region Implementation

	private List<Transform> Initialize(ObjectType type)
	{
		List<Transform> ret = new List<Transform>();

		foreach (Transform child in this.transform)
		{
			if (child.name.ToUpper() == type.ToString())
			{
				ret.Add(child);
			}
		}
		return ret;
	}

#endregion
}
