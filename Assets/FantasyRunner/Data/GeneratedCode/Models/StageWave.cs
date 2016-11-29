///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Stage_Wave
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class StageWave : IStageWave
	{

			public const string TableName = "stage_wave";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_Enemies = "Enemies";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Enemies.
			/// </summary>
			private string _enemies;


			/// <summary>
			/// Initializes a new instance of the <see cref="StageWave"/> class.
			/// </summary>
			public StageWave()
			{
				_id = 0;
				_enemies = "";

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Stage_Wave.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Enemies of this Stage_Wave.
			/// </summary>
			/// <value>The Enemies.</value>
			[JSONField("enemies", "t")]
			public string Enemies
			{
				get;
				set;
			}

			#endregion
	}
}
