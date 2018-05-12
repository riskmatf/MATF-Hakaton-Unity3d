using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour {


	private Text missedText;
	private Text scoreText;

	// Use this for initialization
	void Start () 
	{
		foreach(Transform child in transform.GetComponentInChildren<Transform>())
		{
			if(child.gameObject.name.Equals("Missed"))
			{
				missedText = child.gameObject.GetComponent<Text>();
			}
			else if(child.gameObject.name.Equals("Score"))
			{
				scoreText = child.gameObject.GetComponent<Text>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


	public void setMissedText(string t)
	{
		missedText.text = t;
	}

	public void setScoreText(string t)
	{
		scoreText.text = t;
	}
}
