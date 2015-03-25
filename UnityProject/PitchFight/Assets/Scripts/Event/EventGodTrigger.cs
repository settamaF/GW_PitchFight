//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventGodTrigger : EventTrigger 
{
#region Script Parameters
	public GameObject SecretSauce;
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
		GameUI.Get.SetEvent("Elle est où la secréte sauce??", Pedago.GOD);
		ExecuteEvent();
	}

#endregion

	#region Methods
	public override void ExecuteEvent()
	{
		if (SecretSauce)
		{
			GameObject secretSauce = Instantiate(SecretSauce) as GameObject;
			secretSauce.GetComponent<SecretSauce>().Trigger = this;
		}
	}

	public override void EndEvent()
	{
		base.EndEvent();
	}
	#endregion
}
