using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public abstract class ItemUsageController : MonoBehaviour 
{
    [SerializeField] private ObjectMove _objectMove;
    [SerializeField] private ProgressBarController _durationBar;

    private const int FRAMES_TO_WAIT = 2;
    private Dictionary<CharacterController, int> currentCharactersFrames = new Dictionary<CharacterController, int>();
    private List<CharacterController> currentCharacters = new List<CharacterController>();

    private Rigidbody2D rigidBody;
    private Collider2D[] characterColliders;
    protected ItemsBaseController _itemsController;

    public Item Item { get; protected set; }
    public Dictionary<Buff, CharacterController> Buffs { get; protected set; }

    public Buff GetBuffForCharacter(CharacterController character)
    {
        foreach(KeyValuePair<Buff, CharacterController> item in Buffs)
        {
            if (item.Value == character)
            {
                return item.Key;
            }
        }

        return null;
    }

    public virtual void Initialize(Item item)
    {
        this.Buffs = new Dictionary<Buff, CharacterController>();
        this.Item = item;
        this.StartMovement();
        StartCoroutine(this.InitializeCoroutine());
    }

    public void SetController(ItemsBaseController itemsController)
    {
        this._itemsController = itemsController;
    }

    private void StartMovement()
    {
        if(this._objectMove != null)
        {
            this._objectMove.EnableMovement(true);
        }
    }

    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.characterColliders = GetComponents<Collider2D>();
        this.EnableInteraction(false);
    }

    private IEnumerator InitializeCoroutine()
    {
        yield return StartCoroutine(StartAnimation());

        if (this.Item.Duration > 0)
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
        CharacterController character = col.gameObject.GetComponent<CharacterController>();

        if (character != null && (this.Item.CharacterType == CharacterConstants.CharacterType.Character || character.CharacterType == this.Item.CharacterType))
        {
            if (currentCharactersFrames.ContainsKey(character))
            {
                return;
            }

            this.UseOverCharacter(character);
            if (this.Item.DestroyAfterUse)
            {
                Destroy(gameObject);
            }
            else
            {
                currentCharacters.Add(character);
                currentCharactersFrames[character] = FRAMES_TO_WAIT;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CharacterController character = col.gameObject.GetComponent<CharacterController>();

        if (character != null && (this.Item.CharacterType == CharacterConstants.CharacterType.Character || character.CharacterType == this.Item.CharacterType))
        {
            this.FinishOverCharacter(character);
        }
    }

    private IEnumerator WaitForDurationCoroutine()
    {
        this._durationBar.SetValue(1f);
        float time = this.Item.Duration;
        while (true)
        {
            if (time <= 0)
            {
                break;
            }

            time = Mathf.Max(0, time - Time.deltaTime);

            if (this._durationBar != null)
            {
                this._durationBar.SetValue(time / this.Item.Duration);
            }

            yield return 0;
        }
    }

    protected virtual void UseOverCharacter(CharacterController character)
    {
        this.SetBuff(character);
        this.AddBuffToCharacter(character);
    }

    private void SetBuff(CharacterController character)
    {
        if (this.Item.Buff != null)
        {
            Buff buff = new Buff(this.Item.EffectDuration);
            this.Buffs[buff] = character;

            buff.OnRemove += this.RemoveBuff;

            for (int i = 0; i < this.Item.Buff.Length; i++)
            {
                buff.AddEffect(this.Item.Buff[i].AttributeType, this.Item.Buff[i].AttributeModifierValue, this.Item.Buff[i].AttributeModifierType);
            }

            character.AddBuff(buff);
        }
    }

    private void RemoveBuff(Buff buff)
    {
        CharacterController character = this.Buffs[buff];
        this.Buffs.Remove(buff);

        this.RemoveBuffFromCharacter(character);
    }

    protected virtual void AddBuffToCharacter(CharacterController character) {}
    protected virtual void RemoveBuffFromCharacter(CharacterController character) {}

    protected virtual void FinishOverCharacter(CharacterController character){}

    protected virtual void Update()
    {
        for(int i = currentCharacters.Count - 1; i >= 0; i--)
        {
            CharacterController character = currentCharacters[i];
            currentCharactersFrames[character]--;
            if (currentCharactersFrames[character] == 0)
            {
                currentCharactersFrames.Remove(character);
                currentCharacters.Remove(character);
            }
        }
    }
}
