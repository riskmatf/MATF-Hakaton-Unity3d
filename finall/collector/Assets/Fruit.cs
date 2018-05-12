using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour 
{

	public int points;

	private Controler gameControler; 	
	// Use this for initialization
	void Start () 
	{
		gameControler = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controler>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			gameControler.addScore(points);
			Destroy(gameObject, 0);
		}	
		else if(other.gameObject.tag.Equals("Wall"))
		{
			gameControler.addMissed();
			Destroy(gameObject, 0);
		}
	}
}
