using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour 
{
	public Pause pauseController;
	
	void OnMouseDown()
	{
		transform.localScale = new Vector3(0.9f,0.9f,1);
	}
	
	void OnMouseUp()
	{
		transform.localScale = new Vector3(1,1,1);
        pauseController.UnPause();
	}
}
