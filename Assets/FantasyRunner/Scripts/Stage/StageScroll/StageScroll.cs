using UnityEngine;
using System.Collections;

public class StageScroll : MonoBehaviour 
{
    [SerializeField] private float _speed;
    [SerializeField] private float _limit;
    [SerializeField] private BackGroundScroll[] _backGrounds;
    [SerializeField] private BackGroundScroll _ground;

    bool finished = false;
    private BuffManager _buffManager = new BuffManager();

    public void SetBuffManager(BuffManager buffManager)
    {
        this._buffManager = buffManager;
    }

    private float Speed
    {
        get
        {
            return this._buffManager.ModifyAttributeValue(CharacterConstants.AttributeType.GroundSpeed, this._speed);
        }
    }

    public float GroundSpeed
    {
        get
        {
            if (finished)
            {
                return 0;
            }
            return -this._ground.GetSpeed(this.Speed);
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
            this._backGrounds[i].Initialize();
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
            this._backGrounds[i].UpdatePosition(this.Speed, this._limit);
        }
	}
}
