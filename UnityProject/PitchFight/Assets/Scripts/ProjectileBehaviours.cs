using UnityEngine;
using System.Collections;

public class ProjectileBehaviours : MonoBehaviour 
{
	public float projectileSpeed = 5.0f;
	public	GameObject	player;
	void Start ()
	{
	
	}
	
	void Update () 
	{
		this.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * this.projectileSpeed);
		if (this.CheckProjectilePosition())
			Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject != this.player && other.tag == "Player")
		{
			other.GetComponent<PlayerBehaviours>().HitByProjectile();
			Destroy(this.gameObject);
		}
		else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
			Destroy(this.gameObject);
			
	}

	public bool CheckProjectilePosition()
	{
		Vector3 lWorldToScreenPoint = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
		float lXScreenPosPersoRatio = lWorldToScreenPoint.x / Screen.width;
		if ((lXScreenPosPersoRatio < 0.0f || lXScreenPosPersoRatio > 1.0f ))
			return true;
		return false;
	}
}
