using UnityEngine;
using System.Collections;

public class DeathBorderUIHandler : MonoBehaviour
{
	#region Private Parameters

	private static Animator __animator;

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		__animator = GetComponent<Animator>();
	}

	#endregion

	#region Actions

	public static void	LaunchAnimation(string pAnimName)
	{
		__animator.Play(pAnimName);
	}

	#endregion
}
