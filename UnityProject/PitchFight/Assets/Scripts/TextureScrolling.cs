using UnityEngine;
using System.Collections;

public class TextureScrolling : MonoBehaviour
{
	private float	Offset = 0.0f;
	private float	Scroll = 0.1f;
	public float	Speed;

	//public Vector2 Scroll = new Vector2 (0.05f , 0.0f);
	//Vector2 Offset = new Vector2 (0f, 0f);
	
	void Start () 
	{
	
	}
	
 
	void Update ()
	{
		Offset -= (Scroll * Speed);
		//Debug.Log("Offset = " + Offset);
		while (Offset >= 1)
			Offset--;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Offset, 0);
		//GetComponent<Renderer>().material.SetTextureOffset("_MainTex", Offset);
	}
}

