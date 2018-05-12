using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FiledType
{
	Board,
	HomeBlue, HomeRed, HomeYellow, HomeGreen,
	BeginBlue, BeginRed, BeginYellow, BeginGreen
}

public class BoardField : MonoBehaviour {

	public  FiledType ftype;
	public int num;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static public int compare(GameObject a, GameObject b)
	{
		BoardField ab = a.GetComponent<BoardField>();
		BoardField bb = b.GetComponent<BoardField>();

		return ab.num - bb.num;
	}
}
