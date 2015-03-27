using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleButtonHandlerScript : MonoBehaviour
{
	#region Public Parameters

	public GameObject deselectObject;
	public GameObject selectObject;

	#endregion

	#region Unity Callbacks

	private void	Update()
	{
		transform.GetComponent<Button>().OnPointerEnter(null);
		if (Input.GetButtonDown("J1Jump"))
		{
			Click();
		}
	}

	#endregion

	#region Actions

	public void Click()
	{
		selectObject.SetActive(true);
		deselectObject.SetActive(false);
	}

	#endregion
}
