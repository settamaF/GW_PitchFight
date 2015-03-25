using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
	#region Public Parameters

	public PlayerStateVisu	playersStateVisu;

	public GameObject playerPrefab;
	public RectTransform deathBorder;

	public GameObject victoryPanel;
	public GenerateRail generateRailsScript;
	public GenerateDebugCamera generateDebugCamera;
	public float railsDefaultSpeed;

	#endregion

	#region Private Parameters

	private List<bool> __isAlive;
	private List<GameObject> __players;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
	}

	#endregion

	#region Initialization

	public void InitGame(int pNumberOfPlayers)
	{
		InitAliveStates(pNumberOfPlayers);
		InitAllPersos(pNumberOfPlayers);
		victoryPanel.SetActive(false);
		InitRails();
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
			GameObject lPlayer = Instantiate(playerPrefab);
			lPlayer.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().playerNumber = i + 1;
			PlayerDeath lPlayerDeathScript = lPlayer.GetComponent<PlayerDeath>();
			lPlayerDeathScript.deathBorder = deathBorder;
			lPlayerDeathScript.gameState = this;
			__players.Add(lPlayer);
		}
	}

	private void	InitRails()
	{
		generateRailsScript.enabled = true;
		generateDebugCamera.Speed = railsDefaultSpeed;
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
	}

	#endregion

	#region Actions

	public void	SetAliveState(int pPlayerIndex, bool pValue)
	{
		__isAlive[pPlayerIndex] = pValue;
		if (!pValue)
		{
			SetVisu(pPlayerIndex);
			CheckVictoryCondition();
		}
	}

	private int	GetNbPlayerIsAlive()
	{
		int lNbPlayerIsAlive = 0;
		foreach (bool lAliveState in __isAlive)
		{
			if (lAliveState)
				lNbPlayerIsAlive += 1;
		}
		return lNbPlayerIsAlive;
	}

	private void	SetVisu(int pPlayerIndex)
	{
		playersStateVisu.SetPlayerStateVisu(pPlayerIndex);
	}

	private void	CheckVictoryCondition()
	{
		int lNbPlayerIsAlive = GetNbPlayerIsAlive();
		if (lNbPlayerIsAlive == 0 && __players.Count == 1)
			ActiveVictoryPanel("");
		else if (lNbPlayerIsAlive == 0 && __players.Count > 1)
			ActiveVictoryPanel("Match Nul");
		else if (lNbPlayerIsAlive == 1 && __players.Count > 1)
			ActiveVictoryPanel("Player " + GetWinnerIndex() + " win !");
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

	private void	ActiveVictoryPanel(string pText)
	{
		generateRailsScript.enabled = false;
		generateDebugCamera.Speed = 0.0f;
		victoryPanel.SetActive(true);
	}

	#endregion
}
