//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class BackgroundScrolling : MonoBehaviour 
{
#region Script Parameters
	public float Width;

#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
	private float mX;
	private float mY;
	private Vector3 mBackPos;
	private Vector3 mDefaultPos;
#endregion

#region Unity Methods

	void Awake()
	{
		mDefaultPos = this.transform.position;
	}

	void OnBecameInvisible()
	{
		mBackPos = gameObject.transform.position;
		mX = mBackPos.x + Width*2;
		mY = mBackPos.y;
		transform.position = new Vector3 (mX, mY, 1f);
	}

	void OnDisable()
	{
		ResetBackground();
	}
#endregion

#region Methods
	public void ResetBackground()
	{
		transform.position = mDefaultPos;
	}
#endregion

#region Implementation

#endregion
}
