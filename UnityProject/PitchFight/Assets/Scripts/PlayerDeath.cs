using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
	#region Public Parameters

	public RectTransform deathBorder;
	public GameState gameState;

	#endregion

	#region Private Parameters

	private float __deathBorderRatio;

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		__deathBorderRatio = deathBorder.rect.xMax / 1920.0f;
	}

	private void	Update()
	{
		Vector3 lWorldToScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
		float lXScreenPosPersoRatio = lWorldToScreenPoint.x / Screen.width;
		float lYScreenPosPersoRatio = lWorldToScreenPoint.y / Screen.height;
		if (lXScreenPosPersoRatio < __deathBorderRatio || lYScreenPosPersoRatio < 0.0f)
		{
			gameState.SetAliveState(transform.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().playerNumber - 1, false);
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}

	#endregion
}
