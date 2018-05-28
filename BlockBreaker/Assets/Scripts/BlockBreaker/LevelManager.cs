using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name)
	{
		Debug.Log("Level load requested for: " + name);
		Bricks.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	public void QuitRequest()
	{
		Debug.Log("Level Quit requested for: ");
		Application.Quit();
	}
	
	public void LoadNextLevel()
	{
		Bricks.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void BrickDestroyed()
	{
		if(Bricks.breakableCount <= 0)
		{
			LoadNextLevel();
		}
	
	}
	
}
