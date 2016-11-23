using System.Collections.Generic;

public class Buff
{
    private Dictionary<CharacterConstants.AttributeType, BuffAttributeModifier> _effects = new Dictionary<CharacterConstants.AttributeType, BuffAttributeModifier>();
    private List<CharacterConstants.AttributeType> _buffAttributes = new List<CharacterConstants.AttributeType>();

    public System.Action<Buff> OnRemove;

    public float Duration { get; private set; }

    public List<CharacterConstants.AttributeType> BuffAttributes
    {
        get
        {
            return this._buffAttributes;
        }
    }

    public Buff(float duration = 0)
    {
        this.Duration = duration;
    }

    public override string ToString()
    {
        foreach(var item in _effects)
        {
            return string.Format("[Buff: Attribute={0}, Modifier={1}, Type={2}, Duration={3}]", 
                item.Key.ToString(),
                item.Value.ModifierValue,
                item.Value.ModifierType.ToString(),
                Duration);    
        }

        return "";
    }

    public void AddEffect(CharacterConstants.AttributeType attributeType, float modifierValue, CharacterConstants.AttributeModifierType modiferType)
    {
        this._effects.Add(attributeType, new BuffAttributeModifier(modifierValue, modiferType));
        this._buffAttributes.Add(attributeType);
    }

    public BuffAttributeModifier GetEffectModifier(CharacterConstants.AttributeType attributeType, CharacterConstants.AttributeModifierType modiferType)
    {
        BuffAttributeModifier attributeModifier;
        if (this._effects.TryGetValue(attributeType, out attributeModifier))
        {
            if (attributeModifier.ModifierType == modiferType)
            {
                return attributeModifier;
            }
        }

        return null;
    }

    public void ModifyEffectValue(CharacterConstants.AttributeType attributeType, float modifierValue)
    {
        BuffAttributeModifier attributeModifier;
        if (this._effects.TryGetValue(attributeType, out attributeModifier))
        {
            attributeModifier.SetValue(modifierValue);
        }
    }
}