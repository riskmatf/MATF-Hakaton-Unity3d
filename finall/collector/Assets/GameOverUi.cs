using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour {

	// Use this for initialization
	private Text score;
	private Text message;

	void Start () {
		foreach(Transform child in GetComponentInChildren<Transform>())	
		{
			if(child.gameObject.name.Equals("Score"))
			{
				score = child.gameObject.GetComponent<Text>();
			}
			else if(child.gameObject.name.Equals("Message"))
			{
				message = child.gameObject.GetComponent<Text>();
			}
			else
			{
				Debug.Log(child.gameObject.name);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void setWin(bool win, int finalScore)
	{
		if(win)
		{
			message.text = "You Won";
		}
		else
		{
			message.text = "You lost";	
		}

		score.text = "Final score: " + finalScore; 

		
	}
}
