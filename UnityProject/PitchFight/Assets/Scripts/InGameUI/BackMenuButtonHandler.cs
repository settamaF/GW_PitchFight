using UnityEngine;
using System.Collections;

public class BackMenuButtonHandler : MonoBehaviour
{
	#region Public Parameters

	public GameObject mainMenu;
	public GameObject game;
	public GameObject victoryPanel;

	public float delayBeforeActivateInput;

	#endregion

	#region Private Parameters

	private float __currentDelay;

	#endregion

	#region Unity Callbacks

	private void Update()
	{
		__currentDelay += Time.deltaTime;
		if (Input.GetButtonDown("J1Jump") && __currentDelay >= delayBeforeActivateInput)
		{
			mainMenu.SetActive(true);
			game.SetActive(false);
			victoryPanel.SetActive(false);
		}
	}

	#endregion

	#region Actions

	public void	ActiveUI()
	{
		__currentDelay = 0.0f;
	}

	#endregion
}
