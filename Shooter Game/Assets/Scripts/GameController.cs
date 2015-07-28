using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public GameObject enemy;
	public GameObject bossEnemy;

	public Vector3 spawnValues;
	public int hazardCount;
	public int enemiesCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text gameOverText;
	public Text highScoreText;
	public CanvasGroup buttonsGroup;

	private int score;

	private bool gameOver;
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
		gameOverText.text = "";
		highScoreText.text = "";
		gameOver = false;

		weaveNumber = 0;
		score = 0;
	}

	void Start ()
	{
		buttonsGroup.alpha = 0f;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{

			Vector3 spawnPosition;
			Quaternion spawnRotation;

			if(weaveNumber % 2 == 0) {
				for (int i = 0; i < hazardCount; i++)
				{
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					spawnRotation = Quaternion.identity;
					Instantiate (hazard, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
			} else if(weaveNumber % 5 == 0) {
				spawnPosition = new Vector3 (0, spawnValues.y, spawnValues.z);
				spawnRotation = Quaternion.identity;
				Instantiate (bossEnemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait * 2);
			} else {
				for (int i = 0; i < enemiesCount; i++)
				{
					spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					spawnRotation = Quaternion.identity;
					Instantiate (enemy, spawnPosition, spawnRotation);
					yield return new WaitForSeconds (spawnWait);
				}
			}

			yield return new WaitForSeconds (waveWait);

			++weaveNumber;

			if(gameOver) {
				buttonsGroup.alpha = 1f;
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

		int currentScore = PlayerPrefs.GetInt("HighScore");

		if (currentScore < score) {
			PlayerPrefs.SetInt ("HighScore", score);
			highScoreText.text = "New High Score: " + PlayerPrefs.GetInt ("HighScore");
		} else {
			highScoreText.text = "Score: " + score;
		}
	}

	public void didPressRestart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public void didPressMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
