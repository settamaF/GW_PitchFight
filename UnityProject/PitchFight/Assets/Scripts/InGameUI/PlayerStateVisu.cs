using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStateVisu : MonoBehaviour
{
	#region Actions

	public void	SetPlayerStateVisu(int pPlayerIndex)
	{
		transform.GetChild(pPlayerIndex).GetComponentInChildren<Text>().enabled = true;
	}

	public void	ResetPlayerStateVisu()
	{
		for (int i = 0; i < 4; i++)
			transform.GetChild(i).GetComponentInChildren<Text>().enabled = false;
	}

	#endregion
}
