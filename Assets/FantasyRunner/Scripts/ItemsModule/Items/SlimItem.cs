using System;
using System.Collections;
using UnityEngine;

public class SlimItem : ItemUsageController 
{
    [SerializeField] public CharacterAnimatorController animatorController;

    private Coroutine attackCoroutine;
    private Slim _slim;

    public override void Initialize(Item item)
    {
        base.Initialize(item);
        this._slim = item.GetItemUsage<Slim>();
    }

    private void Awake()
    {
        animatorController.Idle();
    }

    protected override void UseOverCharacter(CharacterController character)
    {
        base.UseOverCharacter(character);
        StopCoroutine();
        attackCoroutine = StartCoroutine(AttackCoroutine(character));
    }

    protected override void FinishOverCharacter(CharacterController character)
    {
        StopCoroutine();
        animatorController.Idle();
    }

    private void StopCoroutine()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = null;
    }

    private IEnumerator AttackCoroutine(CharacterController enemy)
    {
        yield return 0;
        while(true)
        {
            yield return StartCoroutine(this.animatorController.Attack());

            enemy.ReceiveAttack(-this._slim.Attack);

            if(enemy.IsDead)
            {
                animatorController.Idle();
                break;
            }
            yield return new WaitForSeconds(this._slim.AttackDelay);
        }
    }
}
