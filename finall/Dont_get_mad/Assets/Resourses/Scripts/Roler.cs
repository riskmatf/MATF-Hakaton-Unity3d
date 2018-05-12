using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roler : MonoBehaviour {


	static private List<GameObject> diceObjects = new List<GameObject>();
	static private List<Die_d6> diceScript = new List<Die_d6>();

	static private Vector3 defaultRotation = Vector3.zero;
	static private Vector3 defaultScale = Vector3.one;


	static public void clear()
	{
		foreach(GameObject go in diceObjects)
		{
			Destroy(go, 0);
		}

		diceObjects.Clear();
		diceScript.Clear();
	}


	static public bool valid()
	{
		if(rolling())
		{
			return false;
		}

		foreach(Die_d6 d in diceScript)
		{
			if(0 == d.value)
			{
				return false;
			}
		}
		return true;
	}

	static public bool rolling()
	{
		foreach(Die_d6 d in diceScript)
		{
			if(d.rolling)
			{
				return false;
			}
		}

		return true;
	}

	static public int value()
	{
		int res = 0;
		foreach(Die_d6 d in diceScript)
		{
			res += d.value;
		}

		return res;
	}	


	static public void role(int num, Vector3 pos)
	{
		for(int i = 0; i < num; ++i)
		{
			float k = Random.RandomRange(0.4f, 1f);
			float k1 = Random.RandomRange(0.4f, 1f);
			GameObject g = Dice.prefab("d6", pos, defaultRotation, defaultScale, "d6-blue");
			diceObjects.Add(g);
			diceScript.Add(g.GetComponent<Die_d6>());
			g.GetComponent<Rigidbody>().AddForce(new Vector3(k*-1000, -100, k1*1000));
			g.GetComponent<Rigidbody>().AddTorque(new Vector3(10, 0, 10)*1000);
		}
	}
}
