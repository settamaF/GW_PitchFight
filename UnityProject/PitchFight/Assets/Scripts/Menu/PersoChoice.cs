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
		public bool playing;
		
		public sPlayerPersoChoice(int pCurrentIndex, bool pSelected, bool pPlaying)
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

	public GameObject game8Object;
	public GameObject menuObject;

	#endregion

	#region Private Parameters

	private List<sPlayerPersoChoice> __playerMarker;
	private List<bool> __playerHorizontalInputLock;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		__playerMarker = new List<sPlayerPersoChoice>();
		__playerMarker.Add(new sPlayerPersoChoice(0, false, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false, false));
		__playerMarker.Add(new sPlayerPersoChoice(0, false, false));
		__playerHorizontalInputLock = new List<bool>();
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
		__playerHorizontalInputLock.Add(false);
	}

	private void	Update()
	{
		HorizontalInput();
		SelectedInput();
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

	#endregion

	#region Private Actions

	private void	IncPlayerMarkerIndex(int pPlayerIndex, int pValue)
	{
		__playerMarker[pPlayerIndex].currentIndex = (int)Mathf.Repeat(__playerMarker[pPlayerIndex].currentIndex + pValue, 4.0f);
	}

	private void	SetPlayerMarker(int pPlayerIndex, bool pValue)
	{
		playerMarkerObject[__playerMarker[pPlayerIndex].currentIndex].transform.GetChild(pPlayerIndex).GetComponentInChildren<Text>().enabled = pValue;
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
		playerMarkerObject[__playerMarker[pPlayerIndex].currentIndex].transform.GetChild(pPlayerIndex).GetComponent<Image>().color = pColor;
	}

	public void	UpdateSelectedState(int pPlayerIndex)
	{
		if (!CheckIfPlayerIsPlaying(pPlayerIndex))
			return;
		if (CheckIfPersoHasAlreadyBeenChoose(__playerMarker[pPlayerIndex].currentIndex))
			return;
		SelectPerso(pPlayerIndex);
		if (CheckIfAllPlayerHasPerso())
			GoToGameState();
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

	private bool	CheckIfPersoHasAlreadyBeenChoose(int pCurrentIndex)
	{
		foreach (sPlayerPersoChoice lChoice in __playerMarker)
		{
			if (lChoice.currentIndex == pCurrentIndex && lChoice.selected)
				return true;
		}
		return false;
	}

	private bool	CheckIfAllPlayerHasPerso()
	{
		foreach (sPlayerPersoChoice lChoice in __playerMarker)
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
		game8Object.SetActive(true);
		gameState.InitGame(GetNumberOfPlayers());
		menuObject.SetActive(false);
	}

	private int	GetNumberOfPlayers()
	{
		int lNb = 0;
		foreach (sPlayerPersoChoice lChoice in __playerMarker)
		{
			if (lChoice.selected)
				lNb += 1;
		}
		return lNb;
	}

	#endregion
}
