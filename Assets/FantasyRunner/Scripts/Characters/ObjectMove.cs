using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour 
{
	public float speed;
    public bool useFrontLimit = false;

    private bool _movementEnabled = true;
    private StageScroll _stageScroll;
    private Buff _moveBuff;

    private BuffManager _buffManager;

    private BuffManager BuffManager
    {
        get
        {
            if (this._buffManager == null)
            {
                this.SetBuffManager(new BuffManager());
            }

            return this._buffManager;
        }
    }

    public float Speed
    {
        get
        {
            return this.BuffManager.ModifyAttributeValue(CharacterConstants.AttributeType.Speed, this.speed);
        }
    }

    public void SetBuffManager(BuffManager buffManager)
    {
        this._buffManager = buffManager;
        if(this._moveBuff != null)
        {
            this._buffManager.AddBuff(this._moveBuff);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void AddBuff(Buff buff)
    {
        this.BuffManager.AddBuff(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        this.BuffManager.RemoveBuff(buff);
    }

    public void EnableMovement(bool movementEnabled)
    {
        this._movementEnabled = movementEnabled;
    }

    public void StopMoving()
    {
        this.ChangeMoveBuffValue(0f);
    }

    public void ContinueMoving()
    {
        this.ChangeMoveBuffValue(1f);
    }

    private void ChangeMoveBuffValue(float newValue)
    {
        this._moveBuff.ModifyEffectValue(CharacterConstants.AttributeType.Speed, newValue);
        this.BuffManager.UpdateBuffs();
    }

    private void Awake()
    {
        this._moveBuff = new Buff();
        this._moveBuff.AddEffect(CharacterConstants.AttributeType.Speed, 1f, CharacterConstants.AttributeModifierType.Multiply);
        this._stageScroll = FindObjectOfType<StageScroll>();
        if (this._buffManager != null)
        {
            this._buffManager.AddBuff(this._moveBuff);
        }
    }

	private void Update ()
	{
        if (!_movementEnabled)
        {
            return;
        }

        if (Time.timeScale > 0)
        {
            this.Move(((this.Speed) + this._stageScroll.GroundSpeed) * Time.timeScale);
        }
        if (transform.localPosition.x <= StageConstants.GROUND_BACK_LIMIT_X)
		{
			Destroy(gameObject);
		}
	}

    private void Move(float currentSpeed)
    {
        float x = transform.localPosition.x + currentSpeed;
        if (this.useFrontLimit && x > StageConstants.GROUND_FRONT_LIMIT_X)
        {
            x = StageConstants.GROUND_FRONT_LIMIT_X;
        }

        if (Time.timeScale > 0)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, 0);
        }
    }
}
