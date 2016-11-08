using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour 
{
    [SerializeField] private float maxJump = 10f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float maxHp = 10f;
    [SerializeField] private float baseAttack = 5f;
    [SerializeField] private int diePoints = 10;

    [SerializeField] private ProgressBarController hpBar;
    [SerializeField] private CharacterConstants.CharacterType characterType = CharacterConstants.CharacterType.Enemy;

    [SerializeField] private WeaponController weaponController;
    [SerializeField] private GameObject powerItemIndicator;

    [SerializeField] private AudioSource hitAudioSource;

    public System.Action<Character> OnDie;
    public System.Action<Character> OnRemove;

    private CharacterAnimatorController _animatorController;
    private Rigidbody2D _rigidBody;
    private Collider2D[] _characterColliders;
    private ObjectMove _objectMove;

    private Dictionary<string, Action<Collider2D>> _enterActions;
    private Dictionary<string, Action<Collider2D>> _exitActions;

    private Coroutine _battleCoroutine;

    private BuffManager _buffManager = new BuffManager();
    private bool _powerItemActive = false;
    private CharacterConstants.CharacterState _characterState = CharacterConstants.CharacterState.None;
    private float _hp = 0;

    public bool IsDead
    {
        get
        {
            return this._hp <= 0;
        }
    }

    public int DiePoints
    {
        get
        {
            return this.diePoints;
        }
    }

    public CharacterConstants.CharacterType CharacterType
    {
        get
        {
            return this.characterType;
        }
    }

    public float Attack
    {
        get
        {
            return this._buffManager.ModifyAttributeValue(CharacterConstants.AttributeType.Attack, this.baseAttack);
        }
    }

    public void SetSpeed(float speed)
    {
        this._objectMove.SetSpeed(speed);
    }

    public void Heal(float hp)
    {
        ChangeHp(hp);
    }

    public void FullHeal()
    {
        ChangeHp(this.maxHp - this._hp);
    }

    public void Hit(float hp)
    {
        ChangeHp(hp);
    }

    public void FullHit()
    {
        ChangeHp(-this._hp);
    }

    public void AddWeapon(WeaponItem weaponItem)
    {
        this.weaponController.AttachWeapon(weaponItem);
    }

    public void Move(float x)
    {
        //Jump();
        Vector3 position = this.transform.position;
        this.transform.position = new Vector3(x, position.y, position.z);
    }

    public void Win()
    {
        StopRunning();
        StartCoroutine(this._animatorController.Win());
    }

    public void EnablePowerItem(bool active)
    {
        this._powerItemActive = active;
        this.powerItemIndicator.SetActive(this._powerItemActive);
    }

    public void AddBuff(Buff buff)
    {
        this._buffManager.AddBuff(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        this._buffManager.RemoveBuff(buff);
    }

	void Awake ()
	{
        this._hp = this.maxHp;

        if (this.weaponController != null)
        {
            this.weaponController.Initialize(this._buffManager);
        }

        this._animatorController = GetComponent<CharacterAnimatorController>();
        this._rigidBody = GetComponent<Rigidbody2D>();
        this._objectMove = GetComponent<ObjectMove>();
        this._characterColliders = GetComponents<Collider2D>();

        this._objectMove.SetBuffManager(this._buffManager);

        this._enterActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.StartBattle },
            { "Ground", this.Run }
        };

        this._exitActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.FinishBattle }
        };
	}
	
    void Start()
    {
        this.Run();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
        Action<Collider2D> action;
        if(_enterActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
	}

    void OnTriggerExit2D(Collider2D col)
    {
        Action<Collider2D> action;
        if(_exitActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
    }

    bool ChangeState(CharacterConstants.CharacterState newState)
    {
        if (this._characterState == newState)
        {
            return false;
        }

        this._characterState = newState;
        return true;
    }

    void FinishBattle(Collider2D col)
    {
        this.StopBattleCoroutine();
        this.Run(col);
    }

    void StartBattle(Collider2D col)
    {
        if (this.IsDead)
        {
            return;
        }

        Character enemy = col.gameObject.GetComponent<Character>();
        if (enemy == null || enemy.IsDead || this.characterType == enemy.characterType)
        {
            return;
        }

        if (this._powerItemActive)
        {
            enemy.FullHit();
            return;
        }

        if (!this.ChangeState(CharacterConstants.CharacterState.Attacking))
        {
            return;
        }

        this.StopRunning();
        this.StopBattleCoroutine();
        StartCoroutine(BattleCoroutine(enemy));
    }

    void StopBattleCoroutine()
    {
        if (this._battleCoroutine != null)
        {
            StopCoroutine(this._battleCoroutine);
        }

        this._battleCoroutine = null;
    }

    IEnumerator BattleCoroutine(Character enemy)
    {
        //yield return new WaitForSeconds(attackDelay);
        yield return 0;

        while(true)
        {
            yield return StartCoroutine(this._animatorController.Attack());
            if (this.IsDead)
            {
                Run();
                break;
            }

            if (this._powerItemActive)
            {
                enemy.FullHit();
                Run();
                break;
            }

            enemy.ReceiveAttack(-this.Attack);

            if(enemy.IsDead)
            {
                Run();
                break;
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }

    public void ReceiveAttack(float attack)
    {
        this.ChangeHp(attack);
    }

    void ChangeHp(float delta)
    {
        if (delta < 0)
        {
            hitAudioSource.Play();
        }

        this._hp = Mathf.Clamp(this._hp + delta, 0f, maxHp);

        if(this.hpBar != null)
        {
            this.hpBar.SetValue(this._hp / this.maxHp);
        }

        if(this.IsDead)
        {
            this.Die();
        }
    }

    void Die()
    {
        StopAllCoroutines();
        for(int i = 0; i < this._characterColliders.Length; i++)
        {
            this._characterColliders[i].enabled = false;   
        }
        this._rigidBody.Sleep();
        this._objectMove.StopMoving();
        this._animatorController.Die();

        if (this.characterType == CharacterConstants.CharacterType.Enemy)
        {
            hpBar.gameObject.SetActive(false);
        }

        if (this.OnDie != null)
        {
            this.OnDie(this);
        }

        if  (this.characterType != CharacterConstants.CharacterType.Player)
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(CharacterConstants.DESTROY_DELAY_AFTER_DEAD);
        yield return this.transform.DOScale(0, CharacterConstants.DISAPEAR_DURATION).SetEase(Ease.InBack).WaitForCompletion();
        Destroy(this.gameObject);
    }

    void Run(Collider2D col = null)
    {
        if (this.IsDead)
        {
            return;
        }

        if (!this.ChangeState(CharacterConstants.CharacterState.Running))
        {
            return;
        }

        this._objectMove.ContinueMoving();
        this._animatorController.Run();
    }

    void StopRunning()
    {
        this._objectMove.StopMoving();
        this._animatorController.Idle();
    }

    void OnDestroy()
    {
        if (!this.IsDead && this.OnRemove != null)
        {
            this.OnRemove(this);
        }
    }
}
