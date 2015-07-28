using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Text highScoreText;

	//Updates the high score on the main menu
	void Start () {
		highScoreText.text = "Highest Score :" + PlayerPrefs.GetInt ("HighScore");
	}

	//Loads the levelNamed from the main menu
	public void LoadLevel(string levelName)
	{
		Application.LoadLevel(levelName);
	}
}
