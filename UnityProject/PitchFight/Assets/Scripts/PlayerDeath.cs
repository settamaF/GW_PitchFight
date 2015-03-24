using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
	#region Public Parameters

	public Camera referenceCamera;
	public float deathOffset;

	#endregion

	#region Unity Callbacks

	private void	Update()
	{
		//if (referenceCamera.WorldToScreenPoint(transform.position).x < deathOffset)
		//	Debug.Log("Die");
		//else
		//	Debug.Log("Alive");
	}

	#endregion
}
