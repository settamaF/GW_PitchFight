//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class Pattern : MonoBehaviour 
{
#region Script Parameters
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
#endregion

#region Unity Methods
	void Awake()
	{
		mNext = this.transform.FindChild("Next");
		if (!mNext)
		{
			Debug.LogError("Error no child Next in " + this.name);
		}
	}
#endregion

#region Methods
#endregion

#region Implementation
#endregion
}
