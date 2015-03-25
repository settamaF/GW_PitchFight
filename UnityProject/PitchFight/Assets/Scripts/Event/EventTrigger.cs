//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventTrigger : MonoBehaviour 
{
#region Script Parameters
	public int Duration = 3;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	protected bool	Triggered = false;

#endregion

#region Unity Methods
	void Awake()
	{
		Triggered = false;
	}
	
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (Triggered)
			return;
		Triggered = true;
		GameUI.Get.SetEvent("Launch event", Pedago.PEDAGO);
		GameState.get.currentEvent = this;
	}
#endregion

#region Methods
	public virtual void ExecuteEvent()
	{
		return;
	}

	public virtual void EndEvent()
	{
		if (GameUI.Get)
			GameUI.Get.DisableEvent();
	}
#endregion

#region Implementation
#endregion
}
