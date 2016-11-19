using UnityEngine;
using System.Collections;

public class BackGroundScroll : MonoBehaviour 
{
    [SerializeField] private float _speedFactor;

    private Vector3 _firstPos;

    public float GetSpeed(float baseSpeed)
    {
        return baseSpeed * this._speedFactor;   
    }

    public void Initialize()
	{
		this._firstPos = transform.localPosition;
	}
	
    public void UpdatePosition (float speed, float limit)
	{
        if (Time.timeScale > 0)
        {
            transform.localPosition = new Vector3
                (
                    transform.localPosition.x - (this.GetSpeed(speed) * Time.timeScale),
                    transform.localPosition.y,
                    0
                );
        }
		
        if (transform.localPosition.x <= limit)
		{
			transform.localPosition = this._firstPos;
		}
	}
}
