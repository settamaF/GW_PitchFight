//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class Generate : MonoBehaviour 
{
	public enum PatternType
	{
		Pattern4x1 = 0,
		Pattern8x1,
		Pattern36x1,
		PatternRamp,
		PatternRamp2,
	}
#region Script Parameters
	public List<GameObject>	Patterns;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private static int		DEFAULT_COUNT_PATTERN = 5;
	private static float	OFFSET_LEFT = 0f;
	// Private -----------------------------------------------------------------
	private List<Pattern>	mPatterns;
	private float			mSpeed = 0.2f;
#endregion

#region Unity Methods
	void Start ()
	{
		mPatterns = new List<Pattern>();
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
		this.transform.Translate(-mSpeed, 0, 0);
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
			Pattern remove = mPatterns[0];
			mPatterns.RemoveAt(0);
			Destroy(remove.gameObject);
		}
	}
	private void GenerateRandomPattern()
	{
		int i = Random.Range(0, Patterns.Count);

		Pattern ret = CreatePatern((PatternType)i);
		if (ret)
		{
			mPatterns.Add(ret);
			ret.transform.parent = this.transform;
		}
	}
	private Pattern CreatePatern(PatternType pattern)
	{
		GameObject gameObject;

		if ((int)pattern >= Patterns.Count)
		{
			Debug.LogError(pattern.ToString() + " doesn't exist");
			return null;
		}
		if (mPatterns.Count <= 0)
			gameObject = Instantiate(Patterns[(int)pattern], Vector3.zero, Quaternion.identity) as GameObject;
		else
			gameObject = Instantiate(Patterns[(int)pattern], mPatterns[mPatterns.Count - 1].Next.position, Quaternion.identity) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("Error generate pattern " + pattern.ToString());
			return null;
		}
		return gameObject.GetComponent<Pattern>();
	}
#endregion
}
