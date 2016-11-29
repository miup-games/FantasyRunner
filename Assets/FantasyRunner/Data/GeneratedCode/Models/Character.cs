///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Character
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class Character : ICharacter
	{

			public const string TableName = "character";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_PrefabName = "PrefabName";
			public const string PropertyName_Name = "Name";
			public const string PropertyName_CharacterType = "CharacterType";
			public const string PropertyName_AttackDelay = "AttackDelay";
			public const string PropertyName_BaseAttack = "BaseAttack";
			public const string PropertyName_MaxHp = "MaxHp";
			public const string PropertyName_BaseDefenses = "BaseDefenses";
			public const string PropertyName_DiePoints = "DiePoints";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Prefab_Name.
			/// </summary>
			private string _prefabName;
			/// <summary>
			/// The Name.
			/// </summary>
			private string _name;
			/// <summary>
			/// The Character_Type.
			/// </summary>
			private string _characterType;
			/// <summary>
			/// The Attack_Delay.
			/// </summary>
			private float _attackDelay;
			/// <summary>
			/// The Base_Attack.
			/// </summary>
			private int _baseAttack;
			/// <summary>
			/// The Max_Hp.
			/// </summary>
			private int _maxHp;
			/// <summary>
			/// The Base_Defenses.
			/// </summary>
			private int _baseDefenses;
			/// <summary>
			/// The Die_Points.
			/// </summary>
			private int _diePoints;


			/// <summary>
			/// Initializes a new instance of the <see cref="Character"/> class.
			/// </summary>
			public Character()
			{
				_id = 0;
				_prefabName = "";
				_name = "";
				_characterType = "";
				_attackDelay = 0.0f;
				_baseAttack = 0;
				_maxHp = 0;
				_baseDefenses = 0;
				_diePoints = 0;

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Character.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the PrefabName of this Character.
			/// </summary>
			/// <value>The PrefabName.</value>
			[JSONField("prefab_name", "pn")]
			public string PrefabName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Name of this Character.
			/// </summary>
			/// <value>The Name.</value>
			[JSONField("name", "n")]
			public string Name
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the CharacterType of this Character.
			/// </summary>
			/// <value>The CharacterType.</value>
			[JSONField("character_type", "ct")]
			public string CharacterType
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the AttackDelay of this Character.
			/// </summary>
			/// <value>The AttackDelay.</value>
			[JSONField("attack_delay", "ad")]
			public float AttackDelay
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the BaseAttack of this Character.
			/// </summary>
			/// <value>The BaseAttack.</value>
			[JSONField("base_attack", "ba")]
			public int BaseAttack
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the MaxHp of this Character.
			/// </summary>
			/// <value>The MaxHp.</value>
			[JSONField("max_hp", "mhp")]
			public int MaxHp
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the BaseDefenses of this Character.
			/// </summary>
			/// <value>The BaseDefenses.</value>
			[JSONField("base_defenses", "bd")]
			public int BaseDefenses
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the DiePoints of this Character.
			/// </summary>
			/// <value>The DiePoints.</value>
			[JSONField("die_points", "dp")]
			public int DiePoints
			{
				get;
				set;
			}

			#endregion
	}
}
