using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevel : ButtonController 
{
	public string levelName;

	protected override void OnClick()
	{
        base.OnClick();
		Time.timeScale = 1;
		SceneManager.LoadScene(levelName);
	}
}
