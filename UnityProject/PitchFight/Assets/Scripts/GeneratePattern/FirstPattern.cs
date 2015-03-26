//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class FirstPattern : MonoBehaviour 
{
#region Script Parameters
	public List<Transform> Positions;
#endregion

#region Methods
	public void SetStartPosition(List<GameObject> Players)
	{
		if (Players.Count > Positions.Count)
		{	
			Debug.LogError("No position set on pattern " + this.name);
			return;
		}
		for (int i = 0; i < Players.Count; i++)
		{
			Players[i].transform.position = Positions[i].position;
		}
	}
#endregion
}
