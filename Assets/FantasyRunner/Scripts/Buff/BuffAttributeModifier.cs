public class BuffAttributeModifier
{
    public float ModifierValue { get; private set; }
    public CharacterConstants.AttributeModifierType ModifierType { get; private set; }

    public BuffAttributeModifier(float modifierValue, CharacterConstants.AttributeModifierType modifierType)
    {
        this.ModifierValue = modifierValue;
        this.ModifierType = modifierType;
    }

    public void SetValue(float modifierValue)
    {
        this.ModifierValue = modifierValue;
    }
}