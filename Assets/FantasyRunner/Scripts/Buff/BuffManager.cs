using System.Collections.Generic;
using System.Collections;

public class BuffManager
{
    private List<Buff> _buffs = new List<Buff>();
    private Dictionary<CharacterConstants.AttributeType, float> _cachedValues = new Dictionary<CharacterConstants.AttributeType, float>();

    public void AddBuff(Buff buff)
    {
        this.RefreshBuffs(buff);
        this._buffs.Add(buff);

        if (buff.Duration > 0)
        {
            CoroutineManager.instance.StartCoroutine(this.RemoveBuffAfterDuration(buff));
        }
    }

    public void RemoveBuff(Buff buff)
    {
        if(this._buffs.Remove(buff))
        {
            this.RefreshBuffs(buff);
            if(buff.OnRemove != null)
            {
                buff.OnRemove(); 
            }
        }
    }

    public void RefreshBuffs(Buff buff)
    {
        for (int i = 0; i < buff.BuffAttributes.Count; i++)
        {
            this.RefreshBuffs(buff.BuffAttributes[i]);
        }
        //this._finalValues.Clear();
    }

    public void RefreshBuffs(CharacterConstants.AttributeType attributeType)
    {
        this._cachedValues.Remove(attributeType);
        //this._finalValues.Clear();
    }

    public float ModifyAttributeValue(CharacterConstants.AttributeType attributeType, float baseValue)
    {
        float finalValue;
        if (this._cachedValues.TryGetValue(attributeType, out finalValue))
        {
            return this._cachedValues[attributeType];    
        }
        else
        {
            finalValue = this.ModifyAttributeValueInternal(attributeType, baseValue);
            this._cachedValues[attributeType] = finalValue;
            return finalValue;
        }
    }

    private float ModifyAttributeValueInternal(CharacterConstants.AttributeType attributeType, float baseValue)
    {
        BuffAttributeModifier modifier;

        for(int i = 0; i < _buffs.Count; i++)
        {
            modifier = this._buffs[i].GetEffectModifier(attributeType, CharacterConstants.AttributeModifierType.Additive);
            baseValue += (modifier == null ? 0 : modifier.ModifierValue);
        }

        for(int i = 0; i < _buffs.Count; i++)
        {
            modifier = this._buffs[i].GetEffectModifier(attributeType, CharacterConstants.AttributeModifierType.Multiply);
            baseValue *= (modifier == null ? 1f : modifier.ModifierValue);
        }

        return baseValue;
    }

    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        yield return new UnityEngine.WaitForSeconds(buff.Duration);
        this.RemoveBuff(buff);
    }
}