using UnityEngine;
using System.Collections;

public class StageScroll : MonoBehaviour 
{
    [SerializeField] private float _speed;
    [SerializeField] private float _limit;
    [SerializeField] private float _battleSpeedFactor;
    [SerializeField] private BackGroundScroll[] _backGrounds;
    [SerializeField] private BackGroundScroll _ground;

    private bool _finished = false;
    private bool _inBattle = false;
    private BuffManager _buffManager = new BuffManager();

    public void SetBuffManager(BuffManager buffManager)
    {
        this._buffManager = buffManager;
        Buff stageSpeedBuff = new Buff();
        stageSpeedBuff.AddEffect(CharacterConstants.AttributeType.BattleStageSpeed, this._battleSpeedFactor, CharacterConstants.AttributeModifierType.Multiply);
        this._buffManager.AddBuff(stageSpeedBuff);
    }

    private float Speed
    {
        get
        {
            if (this._inBattle)
            {
                return this._buffManager.ModifyAttributeValue(CharacterConstants.AttributeType.BattleStageSpeed, this._speed);
            }

            return this._speed;
        }
    }

    public float GroundSpeed
    {
        get
        {
            if (_finished)
            {
                return 0;
            }
            return -this._ground.GetSpeed(this.Speed);
        }
    }

    public void SetBattle(bool inBattle)
    {
        this._inBattle = inBattle;
    }

    public void StopMovement()
    {
        _finished = true;
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
        if (this._finished)
        {
            return;
        }

        for (int i = 0; i < this._backGrounds.Length; i++)
        {
            this._backGrounds[i].UpdatePosition(this.Speed, this._limit);
        }
	}
}
