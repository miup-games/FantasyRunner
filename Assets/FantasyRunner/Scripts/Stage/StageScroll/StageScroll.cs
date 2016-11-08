using UnityEngine;
using System.Collections;

public class StageScroll : MonoBehaviour 
{
    [SerializeField] private float _speed;
    [SerializeField] private float _limit;
    [SerializeField] private BackGroundScroll[] _backGrounds;
    [SerializeField] private BackGroundScroll _ground;

    bool finished = false;

    public float GroundSpeed
    {
        get
        {
            if (finished)
            {
                return 0;
            }
            return -this._ground.Speed;
        }
    }

    public void StopMovement()
    {
        finished = true;
    }

	void Awake()
	{
        for (int i = 0; i < this._backGrounds.Length; i++)
        {
            this._backGrounds[i].Initialize(this._speed, this._limit);
        }
	}
	
	void LateUpdate ()
	{
        if (this.finished)
        {
            return;
        }

        for (int i = 0; i < this._backGrounds.Length; i++)
        {
            this._backGrounds[i].UpdatePosition();
        }
	}
}
