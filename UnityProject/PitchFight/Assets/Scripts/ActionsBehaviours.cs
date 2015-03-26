using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;


public class ActionsBehaviours : MonoBehaviour 
{
	public Animator		currentAnimator;
	public GameObject	ProjectilePrefab;
	public Transform	ProjectileDummy;
	public float 		currentCoolDownTime;
	public float 		coolDownTimer;
	public bool			ActionDone = false;

	public int 			playerNumber;
	public float		pullingForce;
	public bool 		inArea = false;
	private GameObject	_playerInArea;
	void Start ()
	{

		this.playerNumber = this.transform.parent.gameObject.transform.GetComponent<Platformer2DUserControl>().playerNumber;
	}
	
	void Update () 
	{
		if (this.ActionDone)
		{
			this.currentCoolDownTime++;
			if (currentCoolDownTime > this.coolDownTimer)
			{
				this.currentCoolDownTime = 0.0f;
				this.ActionDone = false;
			}
		}
		if (Input.GetButtonDown("J" + playerNumber.ToString() + "Grab"))
		{
			this.currentAnimator.SetTrigger("Attack");
			if (this.inArea)
				this.GrabAction();
		}
		else if (Input.GetButtonDown("J" + playerNumber.ToString() + "Action") && !this.ActionDone)
		{
			this.currentAnimator.SetTrigger("Throw");
			this.ActionDone = true;
			this.ThrowAction();
		}
	}
	
	public void GrabAction()
	{
		if (this._playerInArea)
			this.StartCoroutine(this.PullPlayer());
		this.inArea = false;
	}

	void ThrowAction()
	{
		GameObject tmpProjectile = Instantiate(ProjectilePrefab, this.ProjectileDummy.position, Quaternion.identity) as GameObject;
		tmpProjectile.GetComponent<ProjectileBehaviours>().player = this.transform.parent.gameObject;
	}

	IEnumerator PullPlayer()
	{
		//player grab animation
		
		float timer = 10.0f;
		float currentTime = 0.0f;
		while (currentTime < timer)
		{
			this._playerInArea.transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * this.pullingForce);
			currentTime++;
			yield return null;
		}
		yield break;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			this.inArea = true;
			this._playerInArea = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			this.inArea = false;
		}
	}
}
