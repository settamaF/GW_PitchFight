using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonHandlerScript : MonoBehaviour
{
	#region Public Parameters

	public GameObject deselectObject;
	public GameObject selectObject;

	#endregion

	#region Actions

	public void	Click()
	{
		selectObject.SetActive(true);
		deselectObject.SetActive(false);
	}

	#endregion
}
