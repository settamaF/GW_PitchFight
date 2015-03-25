//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class GenerateRail : MonoBehaviour 
{
#region Script parameters
#endregion

#region Properties
	private static GenerateRail mInstance;
	public static GenerateRail Get { get { return mInstance; } }

	private List<Object> mPatterns;
	public List<Object> Patterns { get { return mPatterns; } }
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private static int		DEFAULT_COUNT_PATTERN = 5;
	private static float	OFFSET_LEFT = 0f;

	// Private -----------------------------------------------------------------

	private bool			mEvent = false;
	private int				mEventDuration = 0;
	private bool			mSetEventEnd = false;
#endregion

#region Unity Methods
	
	void Awake()
	{
		if (mInstance == null)
			mInstance = this;
	}

	void Update ()
	{
		DeleteLastPattern();
		if (mPatterns.Count < DEFAULT_COUNT_PATTERN)
			GenerateRandomPattern();
	}

#endregion

#region Methods

	public void ActivateRail()
	{
		mPatterns = new List<Object>();
		GenerateFirstPattern();
		for (int i = 0; i < DEFAULT_COUNT_PATTERN; i++)
		{
			GenerateRandomPattern();
		}
		this.enabled = true;
	}

	public void ResetRail()
	{
		if (mEvent)
			GameUI.Get.DisableEvent();
		mEvent = false;
		mSetEventEnd = false;
		mEventDuration = 0;
		while (mPatterns.Count > 0)
		{
			Object remove = mPatterns[0];
			mPatterns.RemoveAt(0);
			remove.Reset();
		}
		this.enabled = false;
	}
#endregion

#region Implementation
	private void DeleteLastPattern()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint(mPatterns[0].Next.position);
		if (viewPos.x < OFFSET_LEFT)
		{
			Object remove = mPatterns[0];
			mPatterns.RemoveAt(0);
			remove.Reset();
		}
	}

	private void GenerateFirstPattern()
	{
		GameObject gameObject = PoolGenerator.Get.GetObject(ObjectType.GROUND, "Ground");

		if (gameObject)
		{
			Object ret = gameObject.GetComponent<Object>();
			if (ret)
			{
				ret.transform.position = Vector3.zero;
				ret.transform.parent = this.transform;
				mPatterns.Add(ret);
			}
		}
	}

	private void GenerateRandomPattern()
	{
		GameObject gameObject = PoolGenerator.Get.GetRandomObject(ObjectType.GROUND);

		if (gameObject)
		{
			Object ret = gameObject.GetComponent<Object>();
			if (ret)
			{
				if (mPatterns.Count <= 0)
				{
					ret.transform.position = Vector3.zero;
				}
				else
				{
					ret.transform.position = mPatterns[mPatterns.Count - 1].Next.position;
				}
				ret.transform.parent = this.transform;
				Vector3 position = ret.transform.position;
				position.z = 0;
				ret.transform.position = position;
				ret.Use(ref mEvent, ref mEventDuration, ref mSetEventEnd);
				mPatterns.Add(ret);
			}
		}
	}
#endregion
}
