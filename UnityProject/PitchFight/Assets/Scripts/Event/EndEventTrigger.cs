//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EndEventTrigger : EventTrigger 
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

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if (Triggered)
			return;
		Triggered = true;
		EndEvent();
	}
#endregion

#region Methods
#endregion

#region Implementation
#endregion
}
