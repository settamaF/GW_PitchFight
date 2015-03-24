//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class GenerateDebugCamera : MonoBehaviour
{
#region Script Parameters
	public float Speed = 0.2f;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Unity Methods
	void Start () 
	{
	}
	
	void Update () 
	{
		this.transform.Translate(Speed, 0, 0);
	}
#endregion

#region Methods
#endregion

#region Implementation
#endregion
}
