using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLevel : MonoBehaviour {

	public void LoadLevel(string levelName)
	{
		Application.LoadLevel(levelName);
	}
}
