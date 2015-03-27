using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VictoryPanelHandler : MonoBehaviour
{
	#region Public Parameters

	public Text textObject;
	public Image persoPicture;

	public List<Sprite> playerPicturesList;

	#endregion

	#region Actions

	public void	ActiveUI(string pTxt, PersoChoice.ePlayerClass playerClass)
	{
		textObject.text = GetVictoryTxt(playerClass);
		if (playerClass != PersoChoice.ePlayerClass.__NONE__)
			persoPicture.sprite = playerPicturesList[(int)playerClass];
		else
			persoPicture.sprite = null;
	}

	private string	GetVictoryTxt(PersoChoice.ePlayerClass pPlayerClass)
	{
		switch (pPlayerClass)
		{
			case PersoChoice.ePlayerClass.__GA__:
				return "Je te low-polize";
			case PersoChoice.ePlayerClass.__GD__:
				return "J'ai toujours raison";
			case PersoChoice.ePlayerClass.__GP__:
				return "Je vous ai rm -rf";
			case PersoChoice.ePlayerClass.__MSD__:
				return "Admirez mon beat";
			default:
				return "";
		}
	}

	#endregion
}
