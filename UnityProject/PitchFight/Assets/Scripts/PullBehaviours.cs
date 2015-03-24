using UnityEngine;
using System.Collections;

public class PullBehaviours : MonoBehaviour 
{
	public GameObject	ProjectilePrefab;
	public Transform	ProjectileDummy;

	public int 		playerNumber;
	public float		pullingForce;
	public bool 		inArea = false;
	private GameObject	_playerInArea;
	void Start () 
	{
	
	}
	
	void Update () 
	{
		if (this.inArea && Input.GetButtonDown("J" + playerNumber.ToString() + "Grab"))
			this.GrabAction();
		else if (Input.GetButtonDown("J" + playerNumber.ToString() + "Action"))
		{
			this.ThrowAction();
		}
	}
	
	void GrabAction()
	{
		if (this._playerInArea)
			this.StartCoroutine(this.PullPlayer());
		this.inArea = false;
	}

	void ThrowAction()
	{
		Instantiate(ProjectilePrefab, this.ProjectileDummy.position, Quaternion.identity);
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
		//Debug.Log("TEST = " + other.gameObject.tag);
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
