using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Debug.Log("cao");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Vector3 move = (new Vector3(0,0, 1) - transform.position).normalized;
		move.y = 0;
		move.z = 0;
		other.transform.position += move;
	}	
}
