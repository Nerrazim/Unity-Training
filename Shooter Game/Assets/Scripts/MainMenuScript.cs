using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Text highScoreText;

	void Awake () {
		highScoreText.text = "Highest Score :" + PlayerPrefs.GetInt ("HighScore");
	}
	
	public void LoadLevel(string levelName)
	{
		Application.LoadLevel(levelName);
	}
}
