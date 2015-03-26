//******************************************************************************
// Authors: Frederic SETTAMA
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class MovingRail : MonoBehaviour
{

#region Properties
	public bool Play { get; set; }
	public float Speed { get; set; }
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private const float INTERVAL_TIMER = 5;
	private const float SPEED_CHANGE = 0.01f;

	// Private -----------------------------------------------------------------
	private float mCurrentTimer;
	private float mMaxSpeed;
#endregion

#region Unity Methods

	void Awake()
	{
		ResetMoving();
	}
	
	void Update () 
	{
		if (Play)
		{
			this.transform.Translate(-Speed, 0, 0);
			UpdateSpeed();
			Debug.Log(Speed);
		}
	}
#endregion

#region Methods

	public void PlayMoving(float speed, float maxSpeed)
	{
		Play = true;
		mCurrentTimer = INTERVAL_TIMER;
		Speed = speed;
		mMaxSpeed = maxSpeed;
	}

	public void ResetMoving()
	{
		Play = false;
		Speed = 0;
		mMaxSpeed = 0;
		mCurrentTimer = 0;
	}

#endregion

#region Implementations
	private void UpdateSpeed()
	{
		mCurrentTimer -= Time.deltaTime;
		if (mCurrentTimer <= 0)
		{
			Speed += SPEED_CHANGE;
			Speed = Mathf.Clamp(Speed, Speed, mMaxSpeed);
			mCurrentTimer = INTERVAL_TIMER;
		}
	}
#endregion
}
