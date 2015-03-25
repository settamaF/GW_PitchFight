﻿//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventGATrigger : EventTrigger 
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
		GameUI.Get.SetEvent("Les textures sont pas super...", Pedago.GA);
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
