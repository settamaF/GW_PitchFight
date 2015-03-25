using UnityEngine;
using System.Collections;

public class StartGameButtonHandler : MonoBehaviour
{
	#region Public Parameters

	public GameObject mainMenu;
	public GameObject choicePerso;

	#endregion

	#region Unity Callbacks

	private void	Update()
	{
		if (Input.GetButtonDown("J1Jump"))
		{
			choicePerso.SetActive(true);
			mainMenu.SetActive(false);
		}
	}

	#endregion
}
