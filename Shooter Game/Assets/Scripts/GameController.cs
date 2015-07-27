using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject enemy;

	public Vector3 spawnValues;
	public Vector3 enemySpawnValues;
	public int hazardCount;
	public int enemiesCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	private int score;

	private bool gameOver;
	private bool restart;
	private int weaveNumber;

	private static GameController _instance;

	public static GameController instance
	{
		get
		{
			if(_instance == null) {
				_instance = FindObjectOfType<GameController>();
			}
			return _instance;
		}
	}

	void Awake()
	{
		restartText.text = "";
		gameOverText.text = "";

		gameOver = false;
		restart = false;

		weaveNumber = 0;
		score = 0;
	}

	void Start ()
	{

		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart && Input.GetKeyDown(KeyCode.R)) 
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			if(weaveNumber % 2 == 0) {
				for (int i = 0; i < hazardCount; i++)
				{
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
			} else {
				for (int i = 0; i < enemiesCount; i++)
				{
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = enemy.transform.rotation;
					Instantiate (enemy, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
			}

			yield return new WaitForSeconds (waveWait);

			++weaveNumber;

			if(gameOver) {
				restartText.text = "Press 'R' for restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScore)
	{
		score += newScore;
		UpdateScore ();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
		weaveNumber = 0;
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
