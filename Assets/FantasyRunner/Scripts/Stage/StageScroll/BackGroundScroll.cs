using UnityEngine;
using System.Collections;

public class BackGroundScroll : MonoBehaviour 
{
    [SerializeField] private float _speedFactor;

	private float _limit;
    private Vector3 _firstPos;

    public float Speed { get; private set; }

    public void Initialize(float speed, float limit)
	{
		this._firstPos = transform.localPosition;
        this.Speed = this._speedFactor * speed;
        this._limit = limit;
	}
	
    public void UpdatePosition ()
	{
        if (Time.timeScale > 0)
        {
            transform.localPosition = new Vector3
                (
                    transform.localPosition.x - (this.Speed * Time.timeScale),
                    transform.localPosition.y,
                    0
                );
        }
		
		if (transform.localPosition.x <= this._limit)
		{
			transform.localPosition = this._firstPos;
		}
	}
}
