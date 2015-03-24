//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class GenerateRail : MonoBehaviour 
{

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private static int		DEFAULT_COUNT_PATTERN = 5;
	private static float	OFFSET_LEFT = 0f;
	// Private -----------------------------------------------------------------
	private List<Object>	mPatterns;
	private float			mSpeed = 0.2f;
#endregion

#region Unity Methods
	void Start ()
	{
		mPatterns = new List<Object>();
		for (int i = 0; i < DEFAULT_COUNT_PATTERN; i++)
		{
			GenerateRandomPattern();
		}
	}
	
	void Update ()
	{
		DeleteLastPattern();
		if (mPatterns.Count < DEFAULT_COUNT_PATTERN)
			GenerateRandomPattern();
	}
	
	void FixedUpdate()
	{
		//this.transform.Translate(-mSpeed, 0, 0);
	}
#endregion

#region Methods

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
				ret.Use();
				mPatterns.Add(ret);
			}
		}
	}
#endregion
}
