//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class TestScript : MonoBehaviour
{
#region Script Parameters
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

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.name);
	}

#endregion

#region Methods
#endregion

#region Implementation
#endregion
}
