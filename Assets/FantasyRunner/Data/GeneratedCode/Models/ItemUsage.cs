///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Item_Usage
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class ItemUsage : IItemUsage
	{

			public const string TableName = "item_usage";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_Coins = "Coins";
			public const string PropertyName_Hp = "Hp";
			public const string PropertyName_Attack = "Attack";
			public const string PropertyName_AttackDelay = "AttackDelay";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Coins.
			/// </summary>
			private int _coins;
			/// <summary>
			/// The Hp.
			/// </summary>
			private int _hp;
			/// <summary>
			/// The Attack.
			/// </summary>
			private int _attack;
			/// <summary>
			/// The Attack_Delay.
			/// </summary>
			private float _attackDelay;


			/// <summary>
			/// Initializes a new instance of the <see cref="ItemUsage"/> class.
			/// </summary>
			public ItemUsage()
			{
				_id = 0;
				_coins = 0;
				_hp = 0;
				_attack = 0;
				_attackDelay = 0.0f;

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Item_Usage.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Coins of this Item_Usage.
			/// </summary>
			/// <value>The Coins.</value>
			[JSONField("coins", "c")]
			public int Coins
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Hp of this Item_Usage.
			/// </summary>
			/// <value>The Hp.</value>
			[JSONField("hp", "hp")]
			public int Hp
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Attack of this Item_Usage.
			/// </summary>
			/// <value>The Attack.</value>
			[JSONField("attack", "a")]
			public int Attack
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the AttackDelay of this Item_Usage.
			/// </summary>
			/// <value>The AttackDelay.</value>
			[JSONField("attack_delay", "ad")]
			public float AttackDelay
			{
				get;
				set;
			}

			#endregion
	}
}
