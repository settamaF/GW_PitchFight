using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenuPanelButtonStateUIHandler : MonoBehaviour
{
	public EventSystem eventSys;

	#region Private Parameters

	private int __buttonIndex;
	private bool __inputLocked;

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		__inputLocked = false;
		__buttonIndex = 0;
	}

	private void	Update()
	{
		transform.GetChild(__buttonIndex).GetComponent<Button>().OnPointerEnter(null);
		GetInput();
		GetPushAction();
	}

	#endregion

	#region Inputs

	private void	GetInput()
	{
		int lInput = (int)Input.GetAxis("J1Horizontal");
		if (!__inputLocked && lInput != 0)
		{
			transform.GetChild(__buttonIndex).GetComponent<Button>().OnPointerExit(null);
			__buttonIndex += lInput;
			__buttonIndex = (int)Mathf.Repeat(__buttonIndex, 4);
			transform.GetChild(__buttonIndex).GetComponent<Button>().OnPointerEnter(null);
			__inputLocked = true;
		}
		else if (lInput == 0)
			__inputLocked = false;
	}

	private void	GetPushAction()
	{
		if (Input.GetButtonDown("J1Jump"))
		{
			//transform.GetChild(__buttonIndex).GetComponent<Button>().OnPointerClick(new UnityEngine.EventSystems.PointerEventData(eventSys));
			transform.GetChild(__buttonIndex).SendMessage("Click");
		}
	}

	#endregion
}
