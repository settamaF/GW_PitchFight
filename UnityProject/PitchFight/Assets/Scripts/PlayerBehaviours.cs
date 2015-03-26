using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class PlayerBehaviours : MonoBehaviour 
{
	public MonoBehaviour 	controllerComponent;
	public MonoBehaviour 	InputComponent;
	public MonoBehaviour 	ActionsComponent;
	public float			stunedByProjectileTimer;
	public float			dyingTimer;
	public float			superSayajinTimer;
	public float			stunedSlowSpeed = 10.0f;
	public float			dyingSpeed;
	public int 				playerNumber;
	public float 			supaSayaSpeed;
	public bool				superSayajin = false;

	

	private Rigidbody2D		rigid;
	private BoxCollider2D 	boxCollider;
	private Animator		currentAnimator;
	void Start () 
	{
		this.rigid = this.GetComponent<Rigidbody2D>();
		this.boxCollider = this.GetComponent<BoxCollider2D>();
		this.playerNumber = this.GetComponent<Platformer2DUserControl>().playerNumber;
		this.currentAnimator = this.GetComponent<Animator>();
	}
	
	void Update () 
	{
		//if (Input.GetKeyDown(KeyCode.A))
		//	this.TookTheSecretSauce();
	}

	public void HitByProjectile()
	{	
		if (!this.superSayajin)
		{
			this.StartCoroutine(this.Stuned());
			this.currentAnimator.SetTrigger("Stunned");
			FxManager.Get.Play(FX.HIT_PLAYER, this.transform);
		}
	}

	public void TookTheSecretSauce()
	{
		this.StartCoroutine(this.SuperSayajinMode());
		this.currentAnimator.SetTrigger("Super");
	}

	public void TookDollarMalus()
	{
		this.dyingTimer = 500;
		this.dyingSpeed = 25;
		this.IsDead();
	}

	public void SetControls(bool b)
	{
		this.controllerComponent.enabled = b;
		this.InputComponent.enabled = b;
		this.ActionsComponent.enabled = b;
	}

	IEnumerator Stuned()
	{
		//play hit animation
		this.SetControls(false);
		float currentTime = 0.0f;
		while (currentTime < this.stunedByProjectileTimer)
		{
			this.transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * this.stunedSlowSpeed);
			currentTime++;
			yield return null;
		}
		this.SetControls(true);
		yield break;
	}

	IEnumerator SuperSayajinMode()
	{
		this.superSayajin = true;
		this.currentAnimator.SetBool("SuperState", this.superSayajin);
		this.SetControls(false);
		float currentTime = 0.0f;
		this.rigid.gravityScale = 0;
		this.rigid.velocity = Vector2.zero;
		this.boxCollider.isTrigger = true;
		while (currentTime < this.superSayajinTimer)
		{
			float x = Input.GetAxis("J" + this.playerNumber.ToString() + "Horizontal") * Time.deltaTime * this.supaSayaSpeed;
			float y = Input.GetAxis("J" + this.playerNumber.ToString() + "Vertical") * Time.deltaTime * this.supaSayaSpeed;
			if (this.CheckPlayerPosition(x, y) == true)
				this.transform.Translate(new Vector3(x, y, 0));
			currentTime++;
			yield return null;
		}
		this.boxCollider.isTrigger = false;
		this.rigid.gravityScale = 3;
		this.superSayajin = false;
		this.SetControls(true);
		this.currentAnimator.SetBool("SuperState", this.superSayajin);
		yield break;

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (this.superSayajin && other.tag == "Player")
			other.gameObject.GetComponent<PlayerBehaviours>().HitByProjectile();
	}

	public bool CheckPlayerPosition(float x, float y)
	{
		Vector3 nextPlayerPosition = new Vector3(this.transform.position.x + x, this.transform.position.y + y, this.transform.position.z);
		Vector3 lWorldToScreenPoint = Camera.main.WorldToScreenPoint(nextPlayerPosition);
		float lXScreenPosPersoRatio = lWorldToScreenPoint.x / Screen.width;
		float lYScreenPosPersoRatio = lWorldToScreenPoint.y / Screen.height;
		if ((lXScreenPosPersoRatio > 0.0f && lXScreenPosPersoRatio < 0.95f ) && (lYScreenPosPersoRatio > 0.0f && lYScreenPosPersoRatio < 0.95f))
			return true;
		return false;
	}

	public void IsDead()
	{
		this.currentAnimator.SetTrigger("DeathPedago");
		this.StartCoroutine(this.Dying());
	}


	IEnumerator Dying()
	{
		this.SetControls(false);
		float currentTime = 0.0f;
		while (currentTime < this.dyingTimer)
		{
			this.transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * this.dyingSpeed);
			currentTime++;
			yield return null;
		}
		this.gameObject.SetActive(false);
		yield break;
	}
}
