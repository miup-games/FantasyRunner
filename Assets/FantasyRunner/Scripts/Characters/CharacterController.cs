using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterController : MonoBehaviour 
{
    [SerializeField] private ProgressBarController hpBar;
    [SerializeField] private AccesoryController weaponController;
    [SerializeField] private AccesoryController armorController;
    [SerializeField] private GameObject powerItemIndicator;

    public System.Action OnAttackStart;
    public System.Action OnRunStart;
    public System.Action<CharacterController> OnDie;
    public System.Action<CharacterController> OnRemove;

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

    public Character Character { get; private set; }

    public bool IsDead
    {
        get
        {
            return this._hp <= 0;
        }
    }

    private bool CanPerformAction
    {
        get
        {
            return (this._characterState != CharacterConstants.CharacterState.Dead && this._characterState != CharacterConstants.CharacterState.Win);
        }
    }

    public int DiePoints
    {
        get
        {
            return this.Character.DiePoints;
        }
    }

    public CharacterConstants.CharacterType CharacterType
    {
        get
        {
            return this.Character.CharacterType;
        }
    }

    public float Attack
    {
        get
        {
            return this._buffManager.ModifyAttributeValue(CharacterConstants.AttributeType.Attack, this.Character.BaseAttack);
        }
    }

    public float Defense
    {
        get
        {
            return this._buffManager.ModifyAttributeValue(CharacterConstants.AttributeType.Defense, this.Character.BaseDefenses);
        }
    }

    public BuffManager BuffManager
    {
        get
        {
            return this._buffManager;
        }
    }

    public void Initialize(Character character)
    {
        this.Character = character;
        this._hp = this.Character.MaxHp;
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
        ChangeHp(this.Character.MaxHp - this._hp);
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
        this.weaponController.AddAccesory(weaponItem, this);
    }

    public void RemoveWeapon()
    {
        this.weaponController.RemoveAccesory();
    }

    public void AddArmor(ArmorItem armorItem)
    {
        this.armorController.AddAccesory(armorItem, this);
    }

    public void RemoveArmor()
    {
        this.armorController.RemoveAccesory();
    }

    public void Move(float x)
    {
        //Jump();
        Vector3 position = this.transform.position;
        this.transform.position = new Vector3(x, position.y, position.z);
    }

    public void Win()
    {
        this.ChangeState(CharacterConstants.CharacterState.Win);
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

    private void Awake ()
	{
        this._animatorController = GetComponent<CharacterAnimatorController>();
        this._rigidBody = GetComponent<Rigidbody2D>();
        this._objectMove = GetComponent<ObjectMove>();
        this._characterColliders = GetComponents<Collider2D>();

        this._objectMove.SetBuffManager(this._buffManager);

        if (this.weaponController != null)
        {
            this.weaponController.Initialize(this._buffManager);
        }

        if (this.armorController != null)
        {
            this.armorController.Initialize(this._buffManager);
        }

        this._enterActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.StartBattle }
        };

        this._exitActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.FinishBattle }
        };
	}
	
    private void Start()
    {
        this.Run();
    }

    private void OnTriggerEnter2D(Collider2D col)
	{
        Action<Collider2D> action;
        if(_enterActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
	}

    private void OnTriggerExit2D(Collider2D col)
    {
        Action<Collider2D> action;
        if(_exitActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
    }

    private bool ChangeState(CharacterConstants.CharacterState newState)
    {
        if (this._characterState == newState)
        {
            return false;
        }

        this._characterState = newState;
        return true;
    }

    private void FinishBattle(Collider2D col)
    {
        this.StopBattleCoroutine();
        this.Run();
    }

    private void StartBattle(Collider2D col)
    {
        if (!this.CanPerformAction)
        {
            return;
        }

        CharacterController enemy = col.gameObject.GetComponent<CharacterController>();
        if (enemy == null || enemy.IsDead || this.Character.CharacterType == enemy.Character.CharacterType)
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

    private void StopBattleCoroutine()
    {
        if (this._battleCoroutine != null)
        {
            StopCoroutine(this._battleCoroutine);
        }

        this._battleCoroutine = null;
    }

    private IEnumerator BattleCoroutine(CharacterController enemy)
    {
        if (this.OnAttackStart != null)
        {
            this.OnAttackStart();
        }

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
            yield return new WaitForSeconds(this.Character.AttackDelay);
        }
    }

    public void ReceiveAttack(float attack)
    {
        attack *= this.Defense;
        this.ChangeHp(attack);
    }

    private void ChangeHp(float delta)
    {
        if (delta < 0)
        {
            AudioManager.instance.PlayFx("Hit");
        }

        this._hp = Mathf.Clamp(this._hp + delta, 0f, this.Character.MaxHp);

        if(this.hpBar != null)
        {
            this.hpBar.SetValue(this._hp / this.Character.MaxHp);
        }

        if(this.IsDead)
        {
            this.Die();
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        for(int i = 0; i < this._characterColliders.Length; i++)
        {
            this._characterColliders[i].enabled = false;   
        }
        this._rigidBody.Sleep();
        this._objectMove.StopMoving();
        this._animatorController.Die();

        if (this.Character.CharacterType == CharacterConstants.CharacterType.Enemy)
        {
            hpBar.gameObject.SetActive(false);
        }

        if (this.OnDie != null)
        {
            this.OnDie(this);
        }

        if  (this.Character.CharacterType != CharacterConstants.CharacterType.Player)
        {
            StartCoroutine(DestroyAfterDelay());
        }

        this.ChangeState(CharacterConstants.CharacterState.Dead);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(CharacterConstants.DESTROY_DELAY_AFTER_DEAD);
        yield return this.transform.DOScale(0, CharacterConstants.DISAPEAR_DURATION).SetEase(Ease.InBack).WaitForCompletion();
        Destroy(this.gameObject);
    }

    private void Run()
    {
        if (!this.CanPerformAction)
        {
            return;
        }

        if (!this.ChangeState(CharacterConstants.CharacterState.Running))
        {
            return;
        }

        if (this.OnRunStart != null)
        {
            this.OnRunStart();
        }

        this._objectMove.ContinueMoving();
        this._animatorController.Run();
    }

    private void StopRunning()
    {
        this._objectMove.StopMoving();
        this._animatorController.Idle();
    }

    private void OnDestroy()
    {
        if (!this.IsDead && this.OnRemove != null)
        {
            this.OnRemove(this);
        }
    }
}
