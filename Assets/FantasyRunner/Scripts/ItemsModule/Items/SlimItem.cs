using System;
using System.Collections;
using UnityEngine;

public class SlimItem : ItemUsageController 
{
    [SerializeField] public float attack = 5f;
    [SerializeField] public float attackDelay = 1f;
    [SerializeField] public CharacterAnimatorController animatorController;

    private Coroutine attackCoroutine;

    private void Awake()
    {
        animatorController.Idle();
    }

    protected override void UseOverCharacter(Character character)
    {
        StopCoroutine();
        attackCoroutine = StartCoroutine(AttackCoroutine(character));
    }

    protected override void FinishOverCharacter(Character character)
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

    private IEnumerator AttackCoroutine(Character enemy)
    {
        yield return 0;
        while(true)
        {
            yield return StartCoroutine(this.animatorController.Attack());

            enemy.ReceiveAttack(-this.attack);

            if(enemy.IsDead)
            {
                animatorController.Idle();
                break;
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
