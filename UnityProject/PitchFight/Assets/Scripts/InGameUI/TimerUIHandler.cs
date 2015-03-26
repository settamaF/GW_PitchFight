using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimerUIHandler : MonoBehaviour
{
	#region Private Parameters

	private static float __desiredTime;
	private static float __currentTime;
	private static string __finalTxt;

	private Text	__text;

	#endregion

	public static void	StartTimer(float pTime, string pFinalText)
	{
		__desiredTime = pTime;
		__finalTxt = pFinalText;
		__currentTime = __desiredTime;
	}

	private void	Start()
	{
		__text = transform.GetComponentInChildren<Text>();
	}

	private void	Update()
	{
		__currentTime -= Time.deltaTime;
		if (__currentTime < -1)
			__text.text = "";
		else if (__currentTime < 0)
			__text.text = __finalTxt;
		else
			__text.text = Convert.ToString((int)__currentTime+1);
	}
}
