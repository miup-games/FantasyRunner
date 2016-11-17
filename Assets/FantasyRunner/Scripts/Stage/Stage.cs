using System.Collections.Generic;

public class Stages
{
    public Stage[] StageList;
}

public class Stage
{
    public int Id;
    public string Title;
    public string PrefabName;
    public string MusicName;
    public List<List<StageEnemy>> Waves;
}
