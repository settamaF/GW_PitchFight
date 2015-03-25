//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

//******************************************************************************
public class EventGDTrigger : EventTrigger
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
		base.OnTriggerEnter2D(other);
		GameUI.Get.SetEvent("Les control sont bien mais ...", Pedago.GD);
		ExecuteEvent();
	}

#endregion

#region Methods
	public override void ExecuteEvent()
	{
		Platformer2DUserControl user;
		foreach (var gameObject in GameState.get.players)
		{
			user = gameObject.GetComponent<Platformer2DUserControl>();
			if (user)
				user.GDEvent = true;
		}
	}

	public override void EndEvent()
	{
		base.EndEvent();
		Platformer2DUserControl user;
		foreach (var gameObject in GameState.get.players)
		{
			user = gameObject.GetComponent<Platformer2DUserControl>();
			if (user)
				user.GDEvent = false;
		}
	}
#endregion
}
