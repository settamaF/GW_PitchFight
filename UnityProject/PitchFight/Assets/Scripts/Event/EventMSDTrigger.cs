//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventMSDTrigger : EventTrigger 
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
		GameUI.Get.SetEvent("Un peu de larcen pour le flow", Pedago.MSD);
		ExecuteEvent();
	}

	protected override void Update()
	{

	}
#endregion

#region Methods
	public override void ExecuteEvent()
	{
		DeathBorderUIHandler.LaunchAnimation("Pedago2");
	}

	public override void EndEvent()
	{
		base.EndEvent();

	}
#endregion
}
