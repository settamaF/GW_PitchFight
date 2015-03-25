//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;

//******************************************************************************
public class SecretSauce : MonoBehaviour 
{
#region Script Parameters
	public float		Speed;
	public float		Timer;
#endregion

#region Properties
	public EventTrigger Trigger { get; set; }
#endregion
	#region Fields
	// Private -----------------------------------------------------------------
	private Vector2			mEndPoint;
	private float			mCurrentTimer;
	
#endregion

#region Unity Methods
	
	void Start()
	{
		Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.8f, 0));

		position.z = 0;
		this.transform.position = position;
	}

	void Update()
	{
		mCurrentTimer -= Time.deltaTime;
		if (mCurrentTimer < 0)
		{
			RandomPosition();
			mCurrentTimer = Timer;
		}
		transform.position = Vector2.Lerp(transform.position, mEndPoint, Time.deltaTime * Speed);
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player")
			return;
		var control = other.gameObject.GetComponent<PlayerBehaviours>();
		if (control)
		{
			control.superSayajin = true;
		}
		FxManager.Get.Play(FX.SECRETSAUCE_PICKUP, transform.position, Quaternion.identity);
		Trigger.EndEvent();
		Destroy(this.gameObject);
	}
#endregion

#region Methods
#endregion

#region Implementation
	private void RandomPosition()
	{
		float x = Random.Range(0, 28);
		float y = Random.Range(0, 14);
		mEndPoint = new Vector2(x, y);
	}
#endregion
}
