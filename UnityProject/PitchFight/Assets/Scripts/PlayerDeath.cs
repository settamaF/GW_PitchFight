using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
	#region Public Parameters

	public RectTransform deathBorder;
	public GameState gameState;
	public float distanceOfPedagoArms = 0.05f;

	#endregion

	#region Private Parameters

	private float __deathBorderRatio;

	#endregion

	#region Unity Callbacks

	private void	Start()
	{
		if (deathBorder)
			__deathBorderRatio = deathBorder.rect.xMax / 1920.0f;
	}

	private void	Update()
	{
		Vector3 lWorldToScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
		float lXScreenPosPersoRatio = lWorldToScreenPoint.x / Screen.width;
		//float lYScreenPosPersoRatio = lWorldToScreenPoint.y / Screen.height;
		if (lXScreenPosPersoRatio < __deathBorderRatio + this.distanceOfPedagoArms)
		{
			if (gameState)
				gameState.SetAliveState(transform.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().playerNumber - 1, false);
			if(gameState.GetNbPlayerIsAlive() > 1)
				gameObject.GetComponent<PlayerBehaviours>().IsDead();
		}
	}

	#endregion
}
