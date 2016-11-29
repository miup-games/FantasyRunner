///--------------------------------------------
/// <summary>
/// Automatic Generated Class - Stage
/// Please do not modify
/// Date: 2016-11-28
/// <summary>
///--------------------------------------------
using GiantParticle.MiniJSON.Factories.Reflection;
using MIUPGames.Database.Interfaces;

namespace MIUPGames.Database.Models
{
	public partial class Stage : IStage
	{

			public const string TableName = "stage";
			public const string PropertyName_Id = "Id";
			public const string PropertyName_Title = "Title";
			public const string PropertyName_PrefabName = "PrefabName";
			public const string PropertyName_MusicName = "MusicName";
			public const string PropertyName_Waves = "Waves";


			/// <summary>
			/// The Id.
			/// </summary>
			private ushort _id;
			/// <summary>
			/// The Title.
			/// </summary>
			private string _title;
			/// <summary>
			/// The Prefab_Name.
			/// </summary>
			private string _prefabName;
			/// <summary>
			/// The Music_Name.
			/// </summary>
			private string _musicName;
			/// <summary>
			/// The Waves.
			/// </summary>
			private string _waves;


			/// <summary>
			/// Initializes a new instance of the <see cref="Stage"/> class.
			/// </summary>
			public Stage()
			{
				_id = 0;
				_title = "";
				_prefabName = "";
				_musicName = "";
				_waves = "";

			}

			#region Getters
			/// <summary>
			/// Gets the Id of this Stage.
			/// </summary>
			/// <value>The Id.</value>
			[JSONField("id", "id")]
			public ushort Id
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Title of this Stage.
			/// </summary>
			/// <value>The Title.</value>
			[JSONField("title", "t")]
			public string Title
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the PrefabName of this Stage.
			/// </summary>
			/// <value>The PrefabName.</value>
			[JSONField("prefab_name", "pn")]
			public string PrefabName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the MusicName of this Stage.
			/// </summary>
			/// <value>The MusicName.</value>
			[JSONField("music_name", "mn")]
			public string MusicName
			{
				get;
				set;
			}
			/// <summary>
			/// Gets the Waves of this Stage.
			/// </summary>
			/// <value>The Waves.</value>
			[JSONField("waves", "w")]
			public string Waves
			{
				get;
				set;
			}

			#endregion
	}
}
