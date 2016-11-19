using System;
using UnityEngine;

public abstract class AccesoryItem : ItemUsageController 
{
    [SerializeField] private float accesoryDuration = 5f;
    [SerializeField] private Sprite iconSprite;

    protected Character _character;

    public Buff Buff { get; protected set; }

    public float AccesoryDuration
    {
        get
        {
            return this.accesoryDuration;
        }
    }

    public Sprite IconSprite
    {
        get
        {
            return this.iconSprite;
        }
    }

    private void RemoveAccesory()
    {
        this.Buff.OnRemove -= this.RemoveAccesory;
        this.RemoveAccesoryFromCharacter();
    }

    protected override void UseOverCharacter(Character character)
    {
        this._character = character;
        this.Buff = new Buff(accesoryDuration);
        this.Buff.OnRemove += this.RemoveAccesory;
        this.SetBuff();
        this._character.AddBuff(this.Buff);
        this.AddAccesoryToCharacter();
    }

    protected abstract void AddAccesoryToCharacter();
    protected abstract void SetBuff();
    protected abstract void RemoveAccesoryFromCharacter();

}
