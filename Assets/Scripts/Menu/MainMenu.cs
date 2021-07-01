using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void ContinueGame()
	{
		SceneManager.LoadScene(1);
	}
	public void NewGame()
	{
		PlayerPrefs.SetInt("level", 0);
		SceneManager.LoadScene(1);
	}
	public void Quit()
	{
		Application.Quit();
	}
}
