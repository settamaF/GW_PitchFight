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
	private GameObject mSecretSauce;
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

	protected override void Update()
	{
	}
#endregion

	#region Methods
	public override void ExecuteEvent()
	{
		if (SecretSauce)
		{
			mSecretSauce = Instantiate(SecretSauce) as GameObject;
			mSecretSauce.GetComponent<SecretSauce>().Trigger = this;
		}
	}

	public override void EndEvent()
	{
		Destroy(mSecretSauce);
		base.EndEvent();
	}
	#endregion
}
