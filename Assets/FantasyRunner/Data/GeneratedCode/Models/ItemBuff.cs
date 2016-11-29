///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Item_Buff
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class ItemBuff : IItemBuff
	{

			public const string TableName = "item_buff";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_AttributeType = "AttributeType";
			public const string PropertyName_AttributeModifierType = "AttributeModifierType";
			public const string PropertyName_AttributeModifierValue = "AttributeModifierValue";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Attribute_Type.
			/// </summary>
			private string _attributeType;
			/// <summary>
			/// The Attribute_Modifier_Type.
			/// </summary>
			private string _attributeModifierType;
			/// <summary>
			/// The Attribute_Modifier_Value.
			/// </summary>
			private float _attributeModifierValue;


			/// <summary>
			/// Initializes a new instance of the <see cref="ItemBuff"/> class.
			/// </summary>
			public ItemBuff()
			{
				_id = 0;
				_attributeType = "";
				_attributeModifierType = "";
				_attributeModifierValue = 0.0f;

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Item_Buff.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the AttributeType of this Item_Buff.
			/// </summary>
			/// <value>The AttributeType.</value>
			[JSONField("attribute_type", "at")]
			public string AttributeType
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the AttributeModifierType of this Item_Buff.
			/// </summary>
			/// <value>The AttributeModifierType.</value>
			[JSONField("attribute_modifier_type", "amt")]
			public string AttributeModifierType
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the AttributeModifierValue of this Item_Buff.
			/// </summary>
			/// <value>The AttributeModifierValue.</value>
			[JSONField("attribute_modifier_value", "amv")]
			public float AttributeModifierValue
			{
				get;
				set;
			}

			#endregion
	}
}
