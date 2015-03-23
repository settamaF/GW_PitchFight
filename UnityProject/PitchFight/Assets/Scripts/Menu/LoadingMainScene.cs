using UnityEngine;
using System.Collections;

public class LoadingMainScene : MonoBehaviour
{
	#region Public Action

	public void	LoadScene(string pSceneName)
	{
		Application.LoadLevel(pSceneName);
	}

	#endregion
}
