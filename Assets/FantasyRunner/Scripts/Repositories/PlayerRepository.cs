using UnityEngine;

public static class PlayerRepository
{
    private static string stageKey = "currentStage";
    private static string lastUnlockedStageKey = "lastUnlockedStage";

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SetCurrentStage(int id)
    {
        PlayerPrefs.SetInt(stageKey, id);
    }

    public static int GetCurrentStage()
    {
        return PlayerPrefs.GetInt(stageKey);
    }

    public static void SetLastUnlockedStage(int id)
    {
        PlayerPrefs.SetInt(lastUnlockedStageKey, id);
    }

    public static int GetLastUnlockedStage()
    {
        return PlayerPrefs.GetInt(lastUnlockedStageKey);
    }
}