//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class MovingRail : MonoBehaviour
{
#region Script Parameters
	public float Speed = 0.2f;
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Unity Methods
	
	void Update () 
	{
		this.transform.Translate(Speed, 0, 0);
	}
#endregion
}
