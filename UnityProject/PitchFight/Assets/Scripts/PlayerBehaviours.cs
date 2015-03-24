using UnityEngine;
using System.Collections;

public class PlayerBehaviours : MonoBehaviour 
{
	public MonoBehaviour controllerComponent;
	public MonoBehaviour InputComponent;
	public MonoBehaviour ActionsComponent;
	public float		stunedByProjectileTimer;
	public float		stunedSlowSpeed = 10.0f;

	private bool alive;


	void Start () 
	{
	
	}
	
	void Update () 
	{
	}

	public void HitByProjectile()
	{
		this.StartCoroutine(this.Stuned());
	}

	IEnumerator Stuned()
	{
		//play hit animation
		this.controllerComponent.enabled = false;
		this.InputComponent.enabled = false;
		this.ActionsComponent.enabled = false;
		float currentTime = 0.0f;
		while (currentTime < this.stunedByProjectileTimer)
		{
			this.transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * this.stunedSlowSpeed);
			currentTime++;
			yield return null;
		}
		this.controllerComponent.enabled = true;
		this.InputComponent.enabled = true;
		this.ActionsComponent.enabled = true;
		yield break;
	}



}
