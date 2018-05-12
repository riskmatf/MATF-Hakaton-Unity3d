using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controler : MonoBehaviour {


	public GameObject leftSpawn;
	public GameObject rightSpawn;
	public List<GameObject> gos;
	public float timeBetwenSpawns = 1;
	public float playerSpeed;
	public int maxMissed = 10;
	public int winScore = 200;
	public GameObject gameOverCanvas;
	public GameObject inGameCanvas;

	private int missed = 0;
	private float time;
	private int score = 0; 
	private InGameUi inGameUi;
	private GameOverUi gameOverUi;
	private GameObject player;

	// Use this for initialization
	void Start () 
	{

		time = timeBetwenSpawns;
		player = GameObject.FindGameObjectWithTag("Player");
		inGameUi = inGameCanvas.GetComponent<InGameUi>();
		gameOverUi = gameOverCanvas.GetComponent<GameOverUi>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		time -= Time.deltaTime;	
		if(time < 0)
		{
			float x = Random.RandomRange(leftSpawn.transform.position.x, rightSpawn.transform.position.x);
			float y = leftSpawn.transform.position.y; 
			int obj = Random.Range(0, 3);

			GameObject o = Instantiate(gos[obj], new Vector3(x, y, 1), new Quaternion());
			o.tag = "Friut";
			o.active = true;
			time = timeBetwenSpawns;


		}

		inGameUi.setScoreText("Score: " + score);

		inGameUi.setMissedText("Missed: " + missed);

		if(missed >= maxMissed)
		{
			gameOver(false);
		}
		else if(score >= winScore)
		{
			gameOver(true);
		}

	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			player.transform.Translate(new Vector3(-playerSpeed, 0, 0));
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			player.transform.Translate(new Vector3(playerSpeed, 0, 0));
		}
	}

	public void addScore(int points)
	{
		score += points;
	}

	public void addMissed()
	{
		++missed;
	}

	void gameOver(bool win)
	{
		Time.timeScale = 0;
		inGameCanvas.active = false;
		gameOverCanvas.active = true;
		gameOverUi.setWin(win, score);		
	}

	public void playAgain()
	{
		score = 0;
		missed = 0;
		gameOverCanvas.active = false;
		inGameCanvas.active = true;
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Friut"))
		{
			Destroy(o, 0);
		}
		Time.timeScale = 1;
	}

	public void mainMenu()
	{
		SceneManager.LoadScene("mainMeni", LoadSceneMode.Single);
	}
}
