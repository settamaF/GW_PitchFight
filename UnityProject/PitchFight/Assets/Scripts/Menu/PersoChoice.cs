﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PersoChoice : MonoBehaviour
{
	#region Public Enum

	public enum ePlayerClass
	{
		__NONE__ = -1,
		__GD__ = 0,
		__MSD__,
		__GA__,
		__GP__
	}

	#endregion

	#region Public Struct

	public class cPlayerPersoChoice
	{
		public ePlayerClass currentIndex;
		public bool selected;
		public bool playing;
		
		public cPlayerPersoChoice(ePlayerClass pCurrentIndex, bool pSelected, bool pPlaying)
		{
			currentIndex = pCurrentIndex;
			selected = pSelected;
			playing = pPlaying;
		}
	}

	#endregion

	#region Public Parameters

	public GameState gameState;

	public List<GameObject> playerMarkerObject;

	public GameObject mainGameObject;
	public GameObject mainMenuObject;
	public GameObject menuObject;

	#endregion

	#region Private Parameters

	private List<cPlayerPersoChoice> __playerMarker;
	private List<bool> __playerHorizontalInputLock;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		InitPlayerListChoice();
		InitPlayerInputLock();
	}

	private void	Update()
	{
		HorizontalInput();
		SelectedInput();
		ValidatingInput();
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

	private void	SelectedInput()
	{
		for (int i = 0; i < 4; i++)
		{
			if (Input.GetButtonDown("J" + (i + 1) + "Jump"))
				UpdateSelectedState(i);
		}
	}

	private void	ValidatingInput()
	{
		if (Input.GetButtonDown("J1Start"))
			ValidGame();
	}

	#endregion

	#region Initialization

	private void	InitPlayerListChoice()
	{
		if (__playerMarker == null)
			__playerMarker = new List<cPlayerPersoChoice>();
		if (__playerMarker.Count > 0)
			__playerMarker.Clear();
		for (int i = 0; i < 4; i++)
			__playerMarker.Add(new cPlayerPersoChoice(0, false, false));
	}

	private void	InitPlayerInputLock()
	{
		if (__playerHorizontalInputLock == null)
			__playerHorizontalInputLock = new List<bool>();
		if (__playerHorizontalInputLock.Count > 0)
			__playerHorizontalInputLock.Clear();
		for (int i = 0; i < 4; i++)
			__playerHorizontalInputLock.Add(false);
	}

	#endregion

	#region Clear

	private void	ResetUI()
	{
		for (int i = 0; i < 4; i++)
		{
			SetPlayerMarker(i, false);
			SetPlayerMarkerToColor(i, Color.white);
		}
		InitPlayerListChoice();
		InitPlayerInputLock();
	}

	#endregion

	#region Private Actions

	private void	IncPlayerMarkerIndex(int pPlayerIndex, int pValue)
	{
		__playerMarker[pPlayerIndex].currentIndex = (ePlayerClass)Mathf.Repeat(((int)__playerMarker[pPlayerIndex].currentIndex) + pValue, 4.0f);
	}

	private void	SetPlayerMarker(int pPlayerIndex, bool pValue)
	{
		playerMarkerObject[((int)__playerMarker[pPlayerIndex].currentIndex)].transform.GetChild(pPlayerIndex).GetComponentInChildren<Image>().enabled = pValue;
	}

	public void	UpdatePlayerMarker(int pPlayerIndex, int pInputValue)
	{
		if (!__playerMarker[pPlayerIndex].selected && __playerMarker[pPlayerIndex].playing)
		{
			SetPlayerMarker(pPlayerIndex, false);
			IncPlayerMarkerIndex(pPlayerIndex, pInputValue);
			SetPlayerMarker(pPlayerIndex, true);
		}
	}

	private void	SetPlayerMarkerToColor(int pPlayerIndex, Color pColor)
	{
		playerMarkerObject[((int)__playerMarker[pPlayerIndex].currentIndex)].transform.GetChild(pPlayerIndex).GetComponent<Image>().color = pColor;
	}

	public void	UpdateSelectedState(int pPlayerIndex)
	{
		if (!CheckIfPlayerIsPlaying(pPlayerIndex))
			return;
		if (!__playerMarker[pPlayerIndex].selected && CheckIfPersoHasAlreadyBeenChoose(__playerMarker[pPlayerIndex].currentIndex))
			return;
		SelectPerso(pPlayerIndex);
	}

	private bool	CheckIfPlayerIsPlaying(int pPlayerIndex)
	{
		if (!__playerMarker[pPlayerIndex].playing)
		{
			__playerMarker[pPlayerIndex].playing = true;
			UpdatePlayerMarker(pPlayerIndex, 0);
			return false;
		}
		return true;
	}

	private void	SelectPerso(int pPlayerIndex)
	{
		__playerMarker[pPlayerIndex].selected = !__playerMarker[pPlayerIndex].selected;
		if (__playerMarker[pPlayerIndex].selected)
			SetPlayerMarkerToColor(pPlayerIndex, new Color(1.0f, 1.0f, 0.0f));
		else
			SetPlayerMarkerToColor(pPlayerIndex, new Color(1.0f, 1.0f, 1.0f));
	}

	private bool	CheckIfPersoHasAlreadyBeenChoose(ePlayerClass pCurrentIndex)
	{
		foreach (cPlayerPersoChoice lChoice in __playerMarker)
		{
			if (lChoice.currentIndex == pCurrentIndex && lChoice.selected)
				return true;
		}
		return false;
	}

	private bool	CheckIfAllPlayerHasPerso()
	{
		foreach (cPlayerPersoChoice lChoice in __playerMarker)
		{
			if (!lChoice.playing)
				continue;
			if (!lChoice.selected)
				return false;
		}
		return true;
	}

	private void	GoToGameState()
	{
		mainGameObject.SetActive(true);
		gameState.InitGame(GetNumberOfPlayers(), GetPlayerClassList());
		ResetUI();
		gameObject.SetActive(false);
		menuObject.SetActive(true);
		mainMenuObject.SetActive(false);
	}

	private int	GetNumberOfPlayers()
	{
		int lNb = 0;
		foreach (cPlayerPersoChoice lChoice in __playerMarker)
		{
			if (lChoice.selected)
				lNb += 1;
		}
		return lNb;
	}

	private void	ValidGame()
	{
		if (GetNumberOfPlayers() <= 0)
			return;
		if (CheckIfAllPlayerHasPerso())
			GoToGameState();
	}

	private List<ePlayerClass>	GetPlayerClassList()
	{
		List<ePlayerClass> lPlayerClassList = new List<ePlayerClass>();

		foreach (cPlayerPersoChoice lChoice in __playerMarker)
		{
			if (!lChoice.playing)
				lPlayerClassList.Add(ePlayerClass.__NONE__);
			else if (lChoice.selected)
				lPlayerClassList.Add(lChoice.currentIndex);
		}

		return lPlayerClassList;
	}

	#endregion
}
