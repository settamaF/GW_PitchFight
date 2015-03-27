using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateVisu : MonoBehaviour
{
	#region Public Parameters

	public List<Sprite> playerClassIconList;

	#endregion

	#region Actions

	public void	SetPlayerStateVisu(List<PersoChoice.ePlayerClass> pPlayerClassList)
	{
		ResetPlayerStateVisu();
		for (int i = 0; i < pPlayerClassList.Count; i++)
		{
			int lIndex = (int)pPlayerClassList[i];
			if (lIndex >= 0)
			{
				transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = playerClassIconList[lIndex];
				transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
			}
		}
	}

	public void	SetPlayerStateDeathVisu(int pPlayerIndex)
	{
		transform.GetChild(pPlayerIndex).GetChild(1).GetComponent<PlayerStateVisuDeathBlinkHandler>().active = true;
	}

	public void	ResetPlayerStateVisu()
	{
		for (int i = 0; i < 4; i++)
		{
			transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
			transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.black;
			transform.GetChild(i).GetChild(1).GetComponent<PlayerStateVisuDeathBlinkHandler>().active = false;
			transform.GetChild(i).GetChild(1).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		}
	}

	#endregion
}
