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

    private CharacterConstants.CharacterState characterState = CharacterConstants.CharacterState.None;
    private float hp = 0;
    private float extraAttck = 0;

	private CharacterAnimatorController animatorController;
    private Rigidbody2D rigidBody;
    private Collider2D[] characterColliders;
    private ObjectMove objectMove;

    private Dictionary<string, Action<Collider2D>> enterActions;
    private Dictionary<string, Action<Collider2D>> exitActions;

    private Coroutine removeWeaponCoroutine;
    private Coroutine battleCoroutine;

    private bool powerItemActive = false;

    public bool IsDead
    {
        get
        {
            return this.hp <= 0;
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

    public void SetSpeed(float speed)
    {
        this.objectMove.SetSpeed(speed);
    }

    public void SetSpeedFactor(float speedFactor, float time = 0)
    {
        this.objectMove.SetSpeedFactor(speedFactor, time);
    }

    public void Heal(float hp)
    {
        ChangeHp(hp);
    }

    public void FullHeal()
    {
        ChangeHp(this.maxHp - this.hp);
    }

    public void Hit(float hp)
    {
        ChangeHp(hp);
    }

    public void FullHit()
    {
        ChangeHp(-this.hp);
    }

    public void AddWeapon(WeaponItem weaponItem)
    {
        this.StopRemoveWeaponCoroutine();
        this.extraAttck = weaponItem.Attack;
        this.weaponController.AttachWeapon(weaponItem);
        this.removeWeaponCoroutine = StartCoroutine(RemoveWeaponAfterDelay(weaponItem.WeaponDuration));
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
        StartCoroutine(this.animatorController.Win());
    }

    public void EnablePowerItem(bool active)
    {
        this.powerItemActive = active;
        this.powerItemIndicator.SetActive(this.powerItemActive);
    }

    void StopRemoveWeaponCoroutine()
    {
        if (this.removeWeaponCoroutine != null)
        {
            StopCoroutine(this.removeWeaponCoroutine);
        }

        this.removeWeaponCoroutine = null;
    }

    IEnumerator RemoveWeaponAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.extraAttck = 0;
        this.weaponController.DetachWeapon();
    }

	void Awake ()
	{
        this.hp = this.maxHp;

        this.animatorController = GetComponent<CharacterAnimatorController>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.objectMove = GetComponent<ObjectMove>();
        this.characterColliders = GetComponents<Collider2D>();

        this.enterActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.StartBattle },
            { "Ground", this.Run },
            { "Obstacle", this.HitObstacle }
        };

        this.exitActions = new Dictionary<string, Action<Collider2D>>()
        {
            { "Character", this.FinishBattle },
            { "Obstacle", this.FinishObstacle }
        };
	}
	
    void Start()
    {
        this.Run();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
        Action<Collider2D> action;
        if(enterActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
	}

    void OnTriggerExit2D(Collider2D col)
    {
        Action<Collider2D> action;
        if(exitActions.TryGetValue(col.tag, out action))
        {
            action(col);
        }
    }

    bool ChangeState(CharacterConstants.CharacterState newState)
    {
        if (this.characterState == newState)
        {
            return false;
        }

        this.characterState = newState;
        return true;
    }

    void HitObstacle(Collider2D col)
    {
        this.StartBattle(col);
    }


    void FinishBattle(Collider2D col)
    {
        this.StopBattleCoroutine();
        this.Run(col);
    }

    void FinishObstacle(Collider2D col)
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

        if (this.powerItemActive)
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
        if (this.battleCoroutine != null)
        {
            StopCoroutine(this.battleCoroutine);
        }

        this.battleCoroutine = null;
    }

    IEnumerator BattleCoroutine(Character enemy)
    {
        //yield return new WaitForSeconds(attackDelay);
        yield return 0;

        while(true)
        {
            yield return StartCoroutine(this.animatorController.Attack());
            if (this.IsDead)
            {
                Run();
                break;
            }

            if (this.powerItemActive)
            {
                enemy.FullHit();
                Run();
                break;
            }

            enemy.ReceiveAttack(-this.baseAttack - this.extraAttck);

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

        this.hp = Mathf.Clamp(this.hp + delta, 0f, maxHp);

        if(this.hpBar != null)
        {
            this.hpBar.SetValue(this.hp / this.maxHp);
        }

        if(this.IsDead)
        {
            this.Die();
        }
    }

    void Die()
    {
        StopAllCoroutines();
        for(int i = 0; i < this.characterColliders.Length; i++)
        {
            this.characterColliders[i].enabled = false;   
        }
        this.rigidBody.Sleep();
        this.objectMove.SetSpeedFactor(0);
        this.animatorController.Die();

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

        this.objectMove.SetSpeedFactor(1f);
        this.animatorController.Run();
    }

    void Jump()
    {
        if (this.IsDead)
        {
            return;
        }

        if (!this.ChangeState(CharacterConstants.CharacterState.Jumping))
        {
            return;
        }

        this.animatorController.Jump();

        Vector3 temp = this.rigidBody.velocity;
        temp.y = 0;
        this.rigidBody.velocity = temp;
        this.rigidBody.AddForce(Vector2.up * maxJump * 50);
    }

    void StopRunning()
    {
        this.objectMove.SetSpeedFactor(0);
        this.animatorController.Idle();
    }

    void OnDestroy()
    {
        if (!this.IsDead && this.OnRemove != null)
        {
            this.OnRemove(this);
        }
    }
}
