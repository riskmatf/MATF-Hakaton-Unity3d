using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
	Blue, Red, Yellow, Green
}

public class Player : MonoBehaviour {

	// Use this for initialization
	public PlayerColor playerColor;
	void Start () {
		
		Color c;

		if(PlayerColor.Blue == playerColor)
		{
			c = Color.blue;
		}
		else if(PlayerColor.Green == playerColor)
		{
			c = Color.green;
		}
		else if(PlayerColor.Yellow == playerColor)
		{
			c = Color.yellow;
		}
		else
		{
			c = Color.red;
		}

		GetComponent<Renderer>().material.color = c;
	}

	public BoardField field()
	{
		Ray r = new Ray(transform.position, Vector3.down);

		RaycastHit hit;

		if(Physics.Raycast(r, out hit))
		{
			return hit.transform.gameObject.GetComponent<BoardField>();
		}
		return null;
	}
}
