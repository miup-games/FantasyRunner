using System.Collections.Generic;

public class Stages
{
    public Stage[] StageList;
}

public class Stage
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PrefabName { get; set; }
    public string MusicName { get; set; }
    public List<List<StageEnemy>> Waves { get; set; }
}
