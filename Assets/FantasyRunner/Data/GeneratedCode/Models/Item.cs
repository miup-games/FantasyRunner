///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Item
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class Item : IItem
	{

			public const string TableName = "item";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_IsInitial = "IsInitial";
			public const string PropertyName_IconName = "IconName";
			public const string PropertyName_PrefabName = "PrefabName";
			public const string PropertyName_Delay = "Delay";
			public const string PropertyName_Cost = "Cost";
			public const string PropertyName_Name = "Name";
			public const string PropertyName_Description = "Description";
			public const string PropertyName_CharacterType = "CharacterType";
			public const string PropertyName_DestroyAfterUse = "DestroyAfterUse";
			public const string PropertyName_EffectDuration = "EffectDuration";
			public const string PropertyName_Duration = "Duration";
			public const string PropertyName_PurchaseCost = "PurchaseCost";
			public const string PropertyName_ItemUsageId = "ItemUsageId";
			public const string PropertyName_ItemBuffIds = "ItemBuffIds";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Is_Initial.
			/// </summary>
			private bool _isInitial;
			/// <summary>
			/// The Icon_Name.
			/// </summary>
			private string _iconName;
			/// <summary>
			/// The Prefab_Name.
			/// </summary>
			private string _prefabName;
			/// <summary>
			/// The Delay.
			/// </summary>
			private float _delay;
			/// <summary>
			/// The Cost.
			/// </summary>
			private int _cost;
			/// <summary>
			/// The Name.
			/// </summary>
			private string _name;
			/// <summary>
			/// The Description.
			/// </summary>
			private string _description;
			/// <summary>
			/// The Character_Type.
			/// </summary>
			private string _characterType;
			/// <summary>
			/// The Destroy_After_Use.
			/// </summary>
			private bool _destroyAfterUse;
			/// <summary>
			/// The Effect_Duration.
			/// </summary>
			private int _effectDuration;
			/// <summary>
			/// The Duration.
			/// </summary>
			private int _duration;
			/// <summary>
			/// The Purchase_Cost.
			/// </summary>
			private int _purchaseCost;
			/// <summary>
			/// The Item_Usage_Id.
			/// </summary>
			private int _itemUsageId;
			/// <summary>
			/// The Item_Buff_Ids.
			/// </summary>
			private string _itemBuffIds;


			/// <summary>
			/// Initializes a new instance of the <see cref="Item"/> class.
			/// </summary>
			public Item()
			{
				_id = 0;
				_isInitial = false;
				_iconName = "";
				_prefabName = "";
				_delay = 0.0f;
				_cost = 0;
				_name = "";
				_description = "";
				_characterType = "";
				_destroyAfterUse = false;
				_effectDuration = 0;
				_duration = 0;
				_purchaseCost = 0;
				_itemUsageId = 0;
				_itemBuffIds = "";

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Item.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the IsInitial of this Item.
			/// </summary>
			/// <value>The IsInitial.</value>
			[JSONField("is_initial", "ii")]
			public bool IsInitial
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the IconName of this Item.
			/// </summary>
			/// <value>The IconName.</value>
			[JSONField("icon_name", "in")]
			public string IconName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the PrefabName of this Item.
			/// </summary>
			/// <value>The PrefabName.</value>
			[JSONField("prefab_name", "pn")]
			public string PrefabName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Delay of this Item.
			/// </summary>
			/// <value>The Delay.</value>
			[JSONField("delay", "d")]
			public float Delay
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Cost of this Item.
			/// </summary>
			/// <value>The Cost.</value>
			[JSONField("cost", "c")]
			public int Cost
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Name of this Item.
			/// </summary>
			/// <value>The Name.</value>
			[JSONField("name", "n")]
			public string Name
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Description of this Item.
			/// </summary>
			/// <value>The Description.</value>
			[JSONField("description", "d")]
			public string Description
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the CharacterType of this Item.
			/// </summary>
			/// <value>The CharacterType.</value>
			[JSONField("character_type", "ct")]
			public string CharacterType
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the DestroyAfterUse of this Item.
			/// </summary>
			/// <value>The DestroyAfterUse.</value>
			[JSONField("destroy_after_use", "dau")]
			public bool DestroyAfterUse
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the EffectDuration of this Item.
			/// </summary>
			/// <value>The EffectDuration.</value>
			[JSONField("effect_duration", "ed")]
			public int EffectDuration
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Duration of this Item.
			/// </summary>
			/// <value>The Duration.</value>
			[JSONField("duration", "du")]
			public int Duration
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the PurchaseCost of this Item.
			/// </summary>
			/// <value>The PurchaseCost.</value>
			[JSONField("purchase_cost", "pc")]
			public int PurchaseCost
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the ItemUsageId of this Item.
			/// </summary>
			/// <value>The ItemUsageId.</value>
			[JSONField("item_usage_id", "iuid")]
			public int ItemUsageId
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the ItemBuffIds of this Item.
			/// </summary>
			/// <value>The ItemBuffIds.</value>
			[JSONField("item_buff_ids", "ibids")]
			public string ItemBuffIds
			{
				get;
				set;
			}

			#endregion
	}
}
