﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
	#region Public Parameters

	public PlayerStateVisu	playersStateVisu;

	public List<GameObject>	playerPrefabList;
	public List<Sprite> playerNbIcon;
	public RectTransform deathBorder;

	public GameObject victoryPanel;
	public GenerateRail generateRailsScript;
	public MovingRail movingRail;
	public float railsDefaultSpeed;
	public float railsMaxSpeed;

	public float delayBeforeStartControle;

	#endregion

	#region Propertys

	private EventTrigger __currentEvent;
	public EventTrigger	currentEvent
	{
		get { return __currentEvent; }
		set { __currentEvent = value; }
	}

	private List<GameObject> __players;
	public List<GameObject> players
	{
		get { return __players; }
	}

	private static GameState __gameState;
	public static GameState	get
	{
		get { return __gameState; }
	}

	#endregion

	#region Private Parameters

	private List<bool> __isAlive;
	private List<PersoChoice.ePlayerClass> __playerClassList;
	private VictoryPanelHandler __victoryPanelHandler;
	private float __currentDelay;
	private bool __controleAlreadyActivate;

	#endregion

	#region Unity Callbacks

	private void	Awake()
	{
		if (__gameState == null)
			__gameState = this;
	}

	private void	Start()
	{
		__victoryPanelHandler = victoryPanel.GetComponent<VictoryPanelHandler>();
	}

	private void	Update()
	{
		__currentDelay += Time.deltaTime;
		if (__currentDelay > delayBeforeStartControle && !__controleAlreadyActivate)
		{
			ActiveControle();
			__controleAlreadyActivate = true;
		}
	}

	#endregion

	#region Initialization

	public void InitGame(int pNumberOfPlayers, List<PersoChoice.ePlayerClass> lPlayerClassList)
	{
		__playerClassList = lPlayerClassList;
		InitAliveStates(pNumberOfPlayers);
		InitAllPersos(pNumberOfPlayers);
		victoryPanel.SetActive(false);
		playersStateVisu.SetPlayerStateVisu(lPlayerClassList);
		InitRails();
		DeactiveControle();
		if (__currentEvent)
			__currentEvent.EndEvent();
		__currentEvent = null;
	}

	private void InitAliveStates(int pNumberOfPlayers)
	{
		if (__isAlive == null)
			__isAlive = new List<bool>();
		if (__isAlive != null && __isAlive.Count > 0)
			__isAlive.Clear();
		for (int i = 0; i < pNumberOfPlayers; i++)
			__isAlive.Add(true);
	}

	private void	InitAllPersos(int pNumberOfPlayers)
	{
		if (__players == null)
			__players = new List<GameObject>();
		if (__players.Count > 0)
			__players.Clear();
		for (int i = 0; i < pNumberOfPlayers; i++)
		{
			GameObject lPlayer = Instantiate(playerPrefabList[(int)__playerClassList[i]]);
			lPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().playerNumber = i + 1;
			lPlayer.transform.FindChild("PlayerInfo").GetComponent<SpriteRenderer>().sprite = playerNbIcon[i];
			PlayerDeath lPlayerDeathScript = lPlayer.GetComponent<PlayerDeath>();
			lPlayerDeathScript.deathBorder = deathBorder;
			lPlayerDeathScript.gameState = this;
			__players.Add(lPlayer);
		}
	}

	private void InitRails()
	{
		generateRailsScript.ActivateRail(__players);
	}

	private void	DeactiveControle()
	{
		for (int i = 0; i < __players.Count; i++)
			__players[i].GetComponent<PlayerBehaviours>().SetControls(false);
		__currentDelay = 0.0f;
		__controleAlreadyActivate = false;
		TimerUIHandler.StartTimer(delayBeforeStartControle, "PITCHEZ");
	}

	private void	ActiveControle()
	{
		for (int i = 0; i < __players.Count; i++)
			__players[i].GetComponent<PlayerBehaviours>().SetControls(true);
		movingRail.PlayMoving(railsDefaultSpeed, railsMaxSpeed);
	}

	#endregion

	#region Clear

	private void	ClearGame()
	{
		__isAlive.Clear();
		foreach (GameObject lObject in __players)
		{
			if (lObject != null)
			{
				lObject.SetActive(false);
				Destroy(lObject);
			}
		}
		__players.Clear();
		__playerClassList.Clear();
	}

	#endregion

	#region Actions

	public void	SetAliveState(int pPlayerIndex, bool pValue)
	{
		__isAlive[pPlayerIndex] = pValue;
		if (!pValue)
		{
			SetDeathVisu(pPlayerIndex);
			CheckVictoryCondition();
		}
	}

	public int	GetNbPlayerIsAlive()
	{
		int lNbPlayerIsAlive = 0;
		foreach (bool lAliveState in __isAlive)
		{
			if (lAliveState)
				lNbPlayerIsAlive += 1;
		}
		return lNbPlayerIsAlive;
	}

	private void	SetDeathVisu(int pPlayerIndex)
	{
		playersStateVisu.SetPlayerStateDeathVisu(pPlayerIndex);
	}

	private void	CheckVictoryCondition()
	{
		int lNbPlayerIsAlive = GetNbPlayerIsAlive();
		if (lNbPlayerIsAlive == 0 && __players.Count == 1)
			ActiveVictoryPanel("", PersoChoice.ePlayerClass.__NONE__);
		else if (lNbPlayerIsAlive == 0 && __players.Count > 1)
			ActiveVictoryPanel("Match Nul", PersoChoice.ePlayerClass.__NONE__);
		else if (lNbPlayerIsAlive == 1 && __players.Count > 1)
		{
			int lWinnerIndex = GetWinnerIndex();
			ActiveVictoryPanel("Player " + (lWinnerIndex + 1) + " win !", __playerClassList[lWinnerIndex]);
		}
	}

	private int	GetWinnerIndex()
	{
		for (int i = 0; i < __isAlive.Count; i++)
		{
			if (__isAlive[i])
				return i;
		}
		return -1;
	}

	private void	ActiveVictoryPanel(string pText, PersoChoice.ePlayerClass pPlayerClass)
	{
		generateRailsScript.ResetRail();
		movingRail.ResetMoving();
		victoryPanel.SetActive(true);
		victoryPanel.GetComponentInChildren<BackMenuButtonHandler>().ActiveUI();
		__victoryPanelHandler.ActiveUI(pText, pPlayerClass);
		ClearGame();
	}

	#endregion
}
