using System.Collections.Generic;
using System.Collections;

public class BuffManager
{
    private List<Buff> _buffs = new List<Buff>();
    private Dictionary<CharacterConstants.AttributeType, float> _finalValues = new Dictionary<CharacterConstants.AttributeType, float>();

    public void AddBuff(Buff buff)
    {
        this.UpdateBuffs();
        this._buffs.Add(buff);

        if (buff.Duration > 0)
        {
            CoroutineManager.instance.StartCoroutine(RemoveBuffAfterDuration(buff));
        }
    }

    public void RemoveBuff(Buff buff)
    {
        this.UpdateBuffs();
        this._buffs.Remove(buff);
    }

    public void UpdateBuffs()
    {
        this._finalValues.Clear();
    }

    public float ModifyAttributeValue(CharacterConstants.AttributeType attributeType, float baseValue)
    {
        float finalValue;
        if (this._finalValues.TryGetValue(attributeType, out finalValue))
        {
            return this._finalValues[attributeType];    
        }
        else
        {
            finalValue = this.ModifyAttributeValueInternal(attributeType, baseValue);
            this._finalValues[attributeType] = finalValue;
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