using UnityEngine;
using System.Collections;

public class Pause : ButtonController
{
	public GameObject pauseMenu;
	public GameObject pauseBtn;

    private float _regularTimeScale;
    private bool _finalPause = false;

    protected override void OnClick()
	{
        base.OnClick();
        this.StartPause();
	}

    public void StartPause(bool finalPause = false)
    {
        this._finalPause = finalPause;
        this._regularTimeScale = Time.timeScale;

        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;

            pauseMenu.SetActive(true);
            pauseBtn.SetActive(false);
        }
    }

    public void UnPause()
    {
        if(!this._finalPause)
        {
            Time.timeScale = this._regularTimeScale;
            pauseBtn.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }
}
