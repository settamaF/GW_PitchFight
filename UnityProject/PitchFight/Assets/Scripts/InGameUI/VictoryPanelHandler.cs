using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryPanelHandler : MonoBehaviour
{
	#region Public Parameters

	public Text textObject;

	#endregion

	#region Actions

	public void	ActiveUI(string pTxt)
	{
		textObject.text = pTxt;
	}

	#endregion
}
