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

	#endregion
}
