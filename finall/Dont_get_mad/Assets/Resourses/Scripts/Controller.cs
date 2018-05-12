using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

	private GameObject playCamera;
	private GameObject roleCamera;
	private GameObject diceSpawnPos;
	private GameObject mainUi;
	private GameObject btnRoll;
	private GameObject txtDiceRoll;

	private List<GameObject> boardFields;
	private List<GameObject> blueHome;
	private List<GameObject> yellowHome;
	private List<GameObject> redHome;
	private List<GameObject> greenHome;
	private List<GameObject> blueBeg;
	private List<GameObject> greenBeg;
	private List<GameObject> yellowBeg;
	private List<GameObject> redBeg;
	

	private PlayerColor playerTurn;
	private int rollNumber;
	private GameObject currentPlayer;
	private GameObject turnIndicator;

	// Use this Ofor initialization
	void Start () 
	{
			playCamera = GameObject.Find("PlayCamera");
			roleCamera = GameObject.Find("RoleCamera");
			diceSpawnPos = GameObject.Find("DiceRolePosition");
			mainUi = GameObject.Find("MainUi");
			playerTurn = PlayerColor.Blue;
			btnRoll = GameObject.Find("btnRoll");
			txtDiceRoll = GameObject.Find("txtDiceRoll");
			turnIndicator = GameObject.Find("Turn");

			turnIndicator.GetComponent<MeshRenderer>().material.color = Color.blue;
			txtDiceRoll.active = false;
			roleCamera.active = false;
			playCamera.active = true;

			GameObject[] gos = GameObject.FindGameObjectsWithTag("BoardField");

			boardFields = new List<GameObject>();
			
			blueHome = new List<GameObject>();
			redHome = new List<GameObject>();
			greenHome = new List<GameObject>();
			yellowHome = new List<GameObject>();

			blueBeg = new List<GameObject>();
			redBeg = new List<GameObject>();
			yellowBeg = new List<GameObject>();
			greenBeg = new List<GameObject>();


			foreach(GameObject g in gos)
			{
				BoardField b = g.GetComponent<BoardField>();

				setActiveField(g, false);
				if(FiledType.Board == b.ftype)
				{
					boardFields.Add(g);
				}
				else if(FiledType.HomeBlue == b.ftype)
				{
					blueHome.Add(g);
				}
				else if(FiledType.HomeGreen == b.ftype)
				{
					greenHome.Add(g);
				}
				else if(FiledType.HomeRed == b.ftype)
				{
					redHome.Add(g);
				}
				else if(FiledType.HomeYellow == b.ftype)
				{
					yellowHome.Add(g);
				}
				else if(FiledType.BeginBlue == b.ftype)
				{
					blueBeg.Add(g);
				}
				else if(FiledType.BeginGreen == b.ftype)
				{
					greenBeg.Add(g);
				}
				else if(FiledType.BeginRed == b.ftype)
				{
					redBeg.Add(g);
				}
				else if(FiledType.BeginYellow == b.ftype)
				{
					yellowBeg.Add(g);
				}
			}			

			boardFields.Sort(BoardField.compare);
			blueHome.Sort(BoardField.compare);
			redHome.Sort(BoardField.compare);
			yellowHome.Sort(BoardField.compare);
			greenHome.Sort(BoardField.compare);

	}
	
	// Update is called once per frame
	void Update () 
	{

		
	}





	public void rollDice()
	{
		mainUi.active = false;
		btnRoll.active = false;
		txtDiceRoll.active = true;

		StartCoroutine("diceRoll");
	}

	IEnumerator diceRoll()
	{
		playCamera.active = false;
		roleCamera.active = true;


		while(true)
		{
			Roler.clear();
			Roler.role(2, diceSpawnPos.transform.position);
			while(Roler.rolling())
			{
				yield return null;
			}
			yield return new WaitForSeconds(1);
			if(Roler.valid())
			{
				break;
			}
		}

		yield return new WaitForSeconds(1);

		roleCamera.active = false;
		playCamera.active = true;
		mainUi.active = true;
		
		rollNumber = Roler.value();
		txtDiceRoll.GetComponent<Text>().text = "You rolled " + rollNumber;
		StartCoroutine("selectPlayer");
	}

	IEnumerator selectPlayer()
	{
		while(true)
		{
			Debug.Log("1");
			while(!Input.GetMouseButtonUp(0))
			{
				Debug.Log("2");
				yield return null;
			}
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);	
			RaycastHit hit;
			Debug.Log("3");


			if(Physics.Raycast(r, out hit, 100))
			{
				Debug.Log("4");
				GameObject target = hit.transform.gameObject;
				Debug.Log(target);

				if(target.tag == "Player")
				{
					Player p = target.GetComponent<Player>();
					if(p.playerColor == playerTurn)
					{
						currentPlayer = target;
						BoardField bf = p.field();
						selectFields(bf);
					}
				}
				else if(target.tag == "BoardField")
				{
					BoardField bf = target.gameObject.GetComponent<BoardField>();
					if(	FiledType.BeginBlue == bf.ftype || FiledType.BeginGreen == bf.ftype 
						|| FiledType.BeginRed == bf.ftype || FiledType.BeginYellow == bf.ftype)
					{
						yield return new WaitForSeconds(1);
						continue;
					}
					else
					{
						setActiveField(target, false);

						Vector3 pos = target.transform.position;
						pos[1] += 1f;
						currentPlayer.transform.position = pos;
						nextPlayer();
					}
					break;
				}
			}
			

			//Run withou this statement boom bad stuff
			yield return null;
		}
	}

	public void nextPlayer()
	{
		StopCoroutine("selectPlayer");
		playerTurn = (PlayerColor)(((int)playerTurn + 1) % 4);
		Debug.Log(playerTurn);
		btnRoll.SetActive(true);
		txtDiceRoll.SetActive(false);

		if(playerTurn == PlayerColor.Blue)
		{
			turnIndicator.GetComponent<MeshRenderer>().material.color = Color.blue;
		}
		else if(playerTurn == PlayerColor.Green)
		{
			turnIndicator.GetComponent<MeshRenderer>().material.color = Color.green;
		}
		else if(playerTurn == PlayerColor.Red)
		{
			turnIndicator.GetComponent<MeshRenderer>().material.color = Color.red;
		}
		else if(playerTurn == PlayerColor.Yellow)
		{
			turnIndicator.GetComponent<MeshRenderer>().material.color = Color.yellow;
		}
	}

	void hideAllFileds()
	{
		foreach(GameObject g in boardFields)
		{
			setActiveField(g, false);
		}
	}
	void selectFields(BoardField bf)
	{
		hideAllFileds();

		if(FiledType.BeginBlue== bf.ftype)
		{
			if(rollNumber > 0)
			{
				setActiveField(boardFields[21], true);
			}
		}
		else if(FiledType.BeginGreen== bf.ftype)
		{
			if(rollNumber == 6)
			{
				setActiveField(boardFields[31], true);
			}
		}
		else if(FiledType.BeginRed== bf.ftype)
		{
			if(rollNumber == 6)
			{
				setActiveField(boardFields[11], true);
			}
		}
		else if(FiledType.BeginYellow== bf.ftype)
		{
			if(rollNumber == 6)
			{
				setActiveField(boardFields[1], true);
			}
		}
		else if(FiledType.Board == bf.ftype)
		{
			int val = bf.num  + rollNumber;

			Debug.Log("cao " + val);
			if(PlayerColor.Blue == playerTurn)
			{
				if(bf.num <= 20 && val > 20)
				{
					val -= 20;
					if(val <= 4 && val > 0)
					{
						if(true) //isfree
						{
							setActiveField(blueHome[val], true);
							return;
						}
						else
						{
							return ;
						}
					}
					else
					{
						return ;
					}
				}
			}
			else if(PlayerColor.Green == playerTurn)
			{
				if(bf.num <= 30  && val > 30)
				{
					val -= 30;
					if(val <= 4 && val > 0)
					{
						if(true) //isfree
						{
							setActiveField(greenHome[val], true);
							return;
						}
						else
						{
							return ;
						}
					}
					else
					{
						return ;
					}
					
				}

			}
			else if(PlayerColor.Red == playerTurn)
			{
				if((bf.num >= 38 || bf.num >= 0 && bf.num <= 10)&& val > 10)
				{
					val -= 10;
					if(val <= 4 && val > 0)
					{
						if(true) //isfree
						{
							setActiveField(redHome[val], true);
							return ;
						}
						else
						{
							return ;
						}
					}
					else
					{
						return ;
					}
				}
			}
			else if(PlayerColor.Yellow == playerTurn)
			{
				if(bf.num <= 40 && val > 40)
				{
					val -= 40;
					if(val <= 4 && val > 0)
					{
						if(true) //isfree
						{
							setActiveField(yellowHome[val], true);
							return ;
						}
						else
						{
							return ;
						}
					}
					else
					{
						return ;
					}
				}
			}
			setActiveField(boardFields[val%40],true);
		}
		//check if in house
	}

	void setActiveField(GameObject g, bool val)
	{
		g.GetComponent<MeshRenderer>().enabled = val;
	}	

	void movePlayer()
	{

	}
}
