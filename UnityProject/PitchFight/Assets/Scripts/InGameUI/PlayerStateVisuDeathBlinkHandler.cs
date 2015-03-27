using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStateVisuDeathBlinkHandler : MonoBehaviour
{
	#region Private Parameters

	private Image __imageObject;
	private float __alpha;

	#endregion

	#region Public Parameters

	public float blinkDelay;

	#endregion

	#region Propertys

	private bool __active;
	public bool active
	{
		set
		{
			__active = value;
			__alpha = 0.0f;
		}
	}

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		__imageObject = GetComponent<Image>();
	}

	private void	Update()
	{
		if (__active)
		{
			__alpha += Time.deltaTime / blinkDelay;
			__imageObject.color = new Color(1.0f, 1.0f, 1.0f, Mathf.PingPong(__alpha, 1.0f));
		}
	}

	#endregion
}
