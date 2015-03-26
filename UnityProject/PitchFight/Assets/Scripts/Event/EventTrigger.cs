//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class EventTrigger : MonoBehaviour 
{
#region Script Parameters
	public int		Duration = 3;
	public float	Timer = 2;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	protected bool		Triggered = false;
	protected float		TimerLaunch;
#endregion

#region Unity Methods
	void Awake()
	{
		Triggered = false;
		TimerLaunch = -1;
	}

	protected virtual void	Start()
	{

	}
	
	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (Triggered)
			return;
		Triggered = true;
		if (GameState.get)
			GameState.get.currentEvent = this;
		TimerLaunch = 0;
	}

	protected virtual void Update()
	{
		if (Triggered && TimerLaunch >= 0)
		{
			TimerLaunch += Time.deltaTime;
			if (TimerLaunch >= Timer)
			{
				ExecuteEvent();
				TimerLaunch = -1;
			}
		}
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
		TimerLaunch = -1;
	}

	public virtual void Reset()
	{
		Triggered = false;
		TimerLaunch = -1;
	}

#endregion

#region Implementation
#endregion
}
