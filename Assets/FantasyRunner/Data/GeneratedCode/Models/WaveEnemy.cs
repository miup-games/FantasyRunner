///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Wave_Enemy
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class WaveEnemy : IWaveEnemy
	{

			public const string TableName = "wave_enemy";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_EnemyName = "EnemyName";
			public const string PropertyName_Delay = "Delay";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Enemy_Name.
			/// </summary>
			private string _enemyName;
			/// <summary>
			/// The Delay.
			/// </summary>
			private float _delay;


			/// <summary>
			/// Initializes a new instance of the <see cref="WaveEnemy"/> class.
			/// </summary>
			public WaveEnemy()
			{
				_id = 0;
				_enemyName = "";
				_delay = 0.0f;

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Wave_Enemy.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the EnemyName of this Wave_Enemy.
			/// </summary>
			/// <value>The EnemyName.</value>
			[JSONField("enemy_name", "en")]
			public string EnemyName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Delay of this Wave_Enemy.
			/// </summary>
			/// <value>The Delay.</value>
			[JSONField("delay", "d")]
			public float Delay
			{
				get;
				set;
			}

			#endregion
	}
}
