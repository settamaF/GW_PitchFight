﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
	#region Public Parameters

	public PlayerStateVisu	playersStateVisu;

	#endregion

	#region Private Parameters

	private List<bool> __isAlive;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		__isAlive = new List<bool>();
		__isAlive.Add(true);
		__isAlive.Add(true);
		__isAlive.Add(true);
		__isAlive.Add(true);
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
		switch (lNbPlayerIsAlive)
		{
			case 0:
				Debug.Log("Match Nul");
				break;
			case 1:
				Debug.Log("GG!");
				break;
			default:
				break;
		}
	}

	#endregion
}