﻿using UnityEngine;
using System.Collections;

public class ProjectileBehaviours : MonoBehaviour 
{
	public float projectileSpeed = 5.0f;
	void Start ()
	{
	
	}
	
	void Update () 
	{
		this.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * this.projectileSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			Destroy(this.gameObject);
	}
}