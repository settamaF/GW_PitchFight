﻿//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventGPTrigger : EventTrigger 
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
		if (Triggered || other.tag != "Player")
			return;
		base.OnTriggerEnter2D(other);
		GameUI.Get.SetEvent("Je crois qu'il y a des erreurs...", Pedago.GP);
		ExecuteEvent();
	}

#endregion

	#region Methods
	public override void ExecuteEvent()
	{

	}

	public override void EndEvent()
	{
		base.EndEvent();

	}
	#endregion
}