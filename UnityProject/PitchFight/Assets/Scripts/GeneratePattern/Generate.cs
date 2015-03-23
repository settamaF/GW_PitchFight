//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class Generate : MonoBehaviour 
{
	public enum Pattern
	{
		Pattern4x1 = 0,
		Pattern8x1,
		Pattern16x1
	}
#region Script Parameters
	public List<GameObject> Patterns;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
	private List<Transform>	mPatterns;
#endregion

#region Unity Methods
	void Start ()
	{
		mPatterns = new List<Transform>();
	}
	
	void Update () 
	{

	}
#endregion

#region Methods

#endregion

#region Implementation
	private void GenerateRandomPattern()
	{
		int i = Random.Range(0, Patterns.Count);

		Transform ret = CreatePatern((Pattern)i);
		if (ret)
			mPatterns.Add(ret);
	}
	private Transform CreatePatern(Pattern pattern)
	{
		GameObject gameObject;

		if ((int)pattern >= Patterns.Count)
		{
			Debug.LogError(pattern.ToString() + " doesn't exist");
		}
		gameObject = GameObject.Instantiate(Patterns[(int)pattern], Vector3.zero, Quaternion.identity) as GameObject;
		if (gameObject == null)
		{
			Debug.LogError("Error generate pattern " + pattern.ToString());
			return null;
		}
		return gameObject.transform;
	}
#endregion
}
