using UnityEngine;
using System.Collections;

public class GPErrorEventUIHandler : MonoBehaviour
{
	#region Public Parameters

	public float delayBetweenError;

	#endregion

	#region Private Parameters

	private float	__currentDelay;
	private bool	__start;
	private int		__index;

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		ResetUI();
	}

	private void	Update()
	{
		if (__start)
		{
			__currentDelay += Time.deltaTime;
			if (__currentDelay >= delayBetweenError)
			{
				__currentDelay = 0.0f;
				transform.GetChild(__index).gameObject.SetActive(true);
				__index = Random.Range(0, transform.childCount);
			}
		}
	}

	#endregion

	#region Actions

	public void	ResetUI()
	{
		__start = false;
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild(i).gameObject.SetActive(false);
	}

	public void	StartUI()
	{
		__start = true;
		__currentDelay = 0.0f;
		__index = Random.Range(0, transform.childCount);
	}

	#endregion
}
