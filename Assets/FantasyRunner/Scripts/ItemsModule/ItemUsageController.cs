﻿using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class ItemUsageController : MonoBehaviour 
{
    [SerializeField] private ObjectMove _objectMove;
    [SerializeField] private bool _destroyAfterUse;
    [SerializeField] public float duration = 0f;
    [SerializeField] private CharacterConstants.CharacterType characterType;
    [SerializeField] private ProgressBarController _durationBar;

    private Rigidbody2D rigidBody;
    private Collider2D[] characterColliders;
    protected ItemsBaseController _itemsController;

    private IEnumerator Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.characterColliders = GetComponents<Collider2D>();

        yield return StartCoroutine(StartAnimation());

        if (duration > 0)
        {
            yield return StartCoroutine(WaitForDurationCoroutine());
            StartCoroutine(this.DestroyAnimation());
        }
    }

    private IEnumerator StartAnimation()
    {
        this.EnableInteraction(false);
        this.transform.localScale = Vector3.zero;
        yield return this.transform.DOScale(1f, ItemConstants.DISAPEAR_DURATION).SetEase(Ease.OutBack).WaitForCompletion();
        this.EnableInteraction(true);
    }

    private IEnumerator DestroyAnimation()
    {
        this.EnableInteraction(false);
        yield return this.transform.DOScale(0f, ItemConstants.DISAPEAR_DURATION).SetEase(Ease.InBack).WaitForCompletion();
    }

    private void EnableInteraction(bool enabled)
    {
        if (this.characterColliders != null)
        {
            for (int i = 0; i < this.characterColliders.Length; i++)
            {
                this.characterColliders[i].enabled = enabled;   
            }
        }

        if(this.rigidBody != null)
        {
            if(enabled) 
            {
                this.rigidBody.WakeUp();
            }
            else
            {
                this.rigidBody.Sleep();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.gameObject.GetComponent<Character>();

        if (character != null && (this.characterType == CharacterConstants.CharacterType.Character || character.CharacterType == this.characterType))
        {
            this.UseOverCharacter(character);
            if (this._destroyAfterUse)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Character character = col.gameObject.GetComponent<Character>();

        if (character != null && (this.characterType == CharacterConstants.CharacterType.Character || character.CharacterType == this.characterType))
        {
            this.FinishOverCharacter(character);
        }
    }

    private IEnumerator WaitForDurationCoroutine()
    {
        this._durationBar.SetValue(1f);
        float time = this.duration;
        while (true)
        {
            if (time <= 0)
            {
                break;
            }

            time = Mathf.Max(0, time - Time.deltaTime);

            if (this._durationBar != null)
            {
                this._durationBar.SetValue(time / this.duration);
            }

            yield return 0;
        }
    }

    protected abstract void UseOverCharacter(Character character);
    protected virtual void FinishOverCharacter(Character character){}

    public void NormalizeSpeed(StageScroll stageScroll)
    {
        if(this._objectMove != null)
        {
            this._objectMove.EnableMovement(true);
        }
    }

    public void SetController(ItemsBaseController itemsController)
    {
        this._itemsController = itemsController;
    }
}