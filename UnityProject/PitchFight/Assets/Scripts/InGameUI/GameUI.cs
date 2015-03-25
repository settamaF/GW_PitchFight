//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************

public enum Pedago
{
	GD = 0,
	GP,
	GA,
	MSD,
	GOD,
	NONE
}

public class GameUI : MonoBehaviour 
{
#region Script Parameters
	public Image[]		Portrait = new Image[4];
	public GameObject	DialogBox;
#endregion

#region Static
#endregion

#region Properties
	private static GameUI mInstance;
	public static GameUI Get { get { return mInstance; } }
#endregion

#region Fields
	// Const -------------------------------------------------------------------
	private const float DEFAULT_ALPHA = 0.5f;

	// Private -----------------------------------------------------------------
	private Image	mActivePedago;
	private Text	mTextDialogBox;
	private bool	mEvent = false;
#endregion

#region Unity Methods
	void Awake()
	{
		if (mInstance != null && mInstance != this)
		{
			Debug.LogWarning("GameUI - we were instantiating a second copy of GameUI , so destroying this instance");
			DestroyImmediate(this, true);
			return;
		}
		DontDestroyOnLoad(this);
		mInstance = this;
	}

	void Start () 
	{
		if (!DialogBox || !InitTextDialogBox())
		{
			Debug.LogError("No dialogBox on GameUI");
			this.enabled = false;
			return;
		}
		mActivePedago = null;
		foreach (var image in Portrait)
		{
			if (image)
			{
				Color tmp = image.color;
				tmp.a = DEFAULT_ALPHA;
				image.color = tmp;
			}
		}
	}
#endregion

#region Methods
	public void SetEvent(string text, Pedago pedago = Pedago.NONE)
	{
		if (!this.enabled)
			return;
		if (mEvent)
			DisableEvent();
		if (pedago != Pedago.NONE)
		{
			if (pedago == Pedago.GOD)
			{
				mActivePedago = null;
			}
			else
			{
				mActivePedago = Portrait[(int)pedago];
				Color tmp = mActivePedago.color;
				tmp.a = 1;
				mActivePedago.color = tmp;
			}
		}
		mTextDialogBox.text = text;
		DialogBox.SetActive(true);
		mEvent = true;
	}

	public void DisableEvent()
	{
		if (!this.enabled)
			return;
		if (mActivePedago)
		{
			Color tmp = mActivePedago.color;
			tmp.a = DEFAULT_ALPHA;
			mActivePedago.color = tmp;
		}
		mActivePedago = null;
		DialogBox.SetActive(false);
		mEvent = false;
	}
#endregion

#region Implementation
	private bool InitTextDialogBox()
	{
		foreach (Transform child in DialogBox.transform)
		{
			if (child.GetComponent<Text>())
			{
				mTextDialogBox = child.GetComponent<Text>();
				break;
			}
		}
		if (!mTextDialogBox)
			return false;
		DialogBox.SetActive(false);
		return true;
	}
#endregion
}
