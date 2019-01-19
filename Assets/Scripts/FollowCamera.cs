using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour 
{
	List<Player> players = new List<Player>();
	public Vector2 offset;

	Vector3 originalPos;

	// Use this for initialization
	void Start () 
	{
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( MenuManager.Instance.isPaused )
			return;
		if (players.Count == 0)
			players.AddRange(FindObjectsOfType<Player>());

		float max = float.NegativeInfinity;
		foreach( Player p in players )
		{
			if (p.transform.position.x > max)
				max = p.transform.position.x;
		}

		if (max > -32 )
			transform.position = Vector3.Lerp(transform.position, new Vector3( max + offset.x, transform.position.y, transform.position.z ), Time.deltaTime);
	}
}