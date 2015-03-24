using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PersoChoice : MonoBehaviour
{
	#region Public Struct

	public class sPlayerPersoChoice
	{
		public int currentIndex;
		public bool selected;
		
		public sPlayerPersoChoice(int pCurrentIndex, bool pSelected)
		{
			currentIndex = pCurrentIndex;
			selected = pSelected;
		}

		public void SetCurrentIndex(int pCurrentIndex)
		{
			this.currentIndex = pCurrentIndex;
		}

		public void	SetSelectedState(bool pSelected)
		{
			this.selected = pSelected;
		}
	}

	#endregion

	#region Public Parameters

	public List<GameObject> playerMarkerObject;

	#endregion

	#region Private Parameters

	private List<sPlayerPersoChoice> __playerMarker;
	private List<bool> __playerHorizontalInputLock;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		__playerMarker = new List<sPlayerPersoChoice>();
		__playerMarker.Add(new sPlayerPersoChoice(0, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false));
		__playerHorizontalInputLock = new List<bool>();
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
	}

	private void	Update()
	{
		HorizontalInput();
	}

	#endregion

	#region Inputs

	private void	HorizontalInput()
	{
		for (int i = 0; i < 4; i++)
		{
			int value = (int)Input.GetAxis("J" + (i+1) + "Horizontal");
			if (value != 0 && !__playerHorizontalInputLock[i])
			{
				UpdatePlayerMarker(i, value);
				__playerHorizontalInputLock[i] = true;
			}
			else if (value == 0)
				__playerHorizontalInputLock[i] = false;
		}
	}

	#endregion

	#region Private Actions

	private void	IncPlayerMarkerIndex(int pPlayerIndex, int pValue)
	{
		int lNewValue = (int)Mathf.Repeat(__playerMarker[pPlayerIndex].currentIndex + pValue, 4.0f);
		__playerMarker[pPlayerIndex].SetCurrentIndex(lNewValue);
	}

	private void	SetPlayerMarker(int pPlayerIndex, bool pValue)
	{
		playerMarkerObject[__playerMarker[pPlayerIndex].currentIndex].transform.GetChild(pPlayerIndex).GetComponentInChildren<Text>().enabled = pValue;
	}

	public void	UpdatePlayerMarker(int pPlayerIndex, int pInputValue)
	{
		if (!__playerMarker[pPlayerIndex].selected)
		{
			SetPlayerMarker(pPlayerIndex, false);
			IncPlayerMarkerIndex(pPlayerIndex, pInputValue);
			SetPlayerMarker(pPlayerIndex, true);
		}
	}

	private void	SetPlayerMarkerToColor(int pPlayerIndex, Color pColor)
	{
		playerMarkerObject[__playerMarker[pPlayerIndex].currentIndex].transform.GetChild(pPlayerIndex).GetComponent<Image>().color = pColor;
	}

	public void	SelectActualPerso(int pPlayerIndex)
	{
		if (!__playerMarker[pPlayerIndex].selected)
		{
			if (AlreadySelected(__playerMarker[pPlayerIndex].currentIndex))
				return;
		}
		__playerMarker[pPlayerIndex].SetSelectedState(!__playerMarker[pPlayerIndex].selected);
		if (__playerMarker[pPlayerIndex].selected)
			SetPlayerMarkerToColor(pPlayerIndex, new Color(1.0f, 1.0f, 0.0f));
		else
			SetPlayerMarkerToColor(pPlayerIndex, new Color(1.0f, 1.0f, 1.0f));
	}

	private bool	AlreadySelected(int pPersoIndex)
	{
		foreach (sPlayerPersoChoice lChoice in __playerMarker)
		{
			if (lChoice.currentIndex == pPersoIndex && lChoice.selected)
				return true;
		}
		return false;
	}

	#endregion
}
