using UnityEngine;
using System.Collections;

public class BackMenuButtonHandler : MonoBehaviour
{
	#region Public Parameters

	public GameObject mainMenu;
	public GameObject game;
	public GameObject victoryPanel;

	#endregion

	#region Unity Callbacks

	private void Update()
	{
		if (Input.GetButtonDown("J1Jump"))
		{
			mainMenu.SetActive(true);
			game.SetActive(false);
			victoryPanel.SetActive(false);
		}
	}

	#endregion
}
