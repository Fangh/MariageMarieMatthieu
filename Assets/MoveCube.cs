using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCube : MonoBehaviour 
{
	public Transform patte1;
	public Transform patte2;
	public float speed;

	private bool isTurningRight = false;
	private bool isTurningLeft = false;

	float objectif;
	int difficulty;

	// Use this for initialization
	void Start () 
	{
		switch (difficulty)
		{
			case 1:
			objectif /= 4;
			break;
			case 2:
			objectif /= 2;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			isTurningRight = !isTurningRight;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			isTurningLeft = !isTurningLeft;
		}

		float a = 2.0513f;
		Debug.Log(a.ToString("F0"));

		if (isTurningRight)
			transform.RotateAround(patte1.position, Vector3.up, 20 * speed);

		if (isTurningLeft)
			transform.RotateAround(patte2.position, Vector3.up, -20 * speed);
	}
}
