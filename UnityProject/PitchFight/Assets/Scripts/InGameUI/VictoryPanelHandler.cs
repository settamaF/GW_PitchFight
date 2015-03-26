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
		textObject.text = pTxt;
		if (playerClass != PersoChoice.ePlayerClass.__NONE__)
			persoPicture.sprite = playerPicturesList[(int)playerClass];
	}

	#endregion
}
