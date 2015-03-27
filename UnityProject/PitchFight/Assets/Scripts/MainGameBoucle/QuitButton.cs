﻿using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour
{
	#region Actions

	public void	Quit()
	{
		Application.Quit();
	}

	public void Click()
	{
		Quit();
	}

	#endregion
}
