using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject pauseBtn;

    public float _regularTimeScale;

	void OnMouseDown()
	{
		transform.localScale = new Vector3(0.9f,0.9f,1);
	}
	
    private void OnMouseUp()
	{
        transform.localScale = new Vector3(1,1,1);
        this.StartPause();
	}

    private void StartPause()
    {
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
        Time.timeScale = this._regularTimeScale;
        pauseBtn.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
