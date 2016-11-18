using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class PlayerRepository
{
    private static string stageKey = "currentStage";
    private static string lastUnlockedStageKey = "lastUnlockedStage";
    private static string itemsKey = "items";

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

    public static List<GamerItem> GetGamerItems()
    {
        Items items = ItemRepository.GetItems();
        List<GamerItem> gamerItems;

        string itemsString = PlayerPrefs.GetString(itemsKey);

        UnityEngine.Debug.LogError("WAAAAAAAAA: " + itemsString);

        bool useInitialSetting = string.IsNullOrEmpty(itemsString);

        if (useInitialSetting)
        {
            gamerItems = new List<GamerItem>();
        }
        else
        {
            gamerItems = JsonConvert.DeserializeObject<GamerItems>(itemsString).ItemList;
        }

        for(int i = 0; i < items.ItemList.Length; i++)
        {
            GamerItem gamerItem = gamerItems.Find((item) => item.ItemId == items.ItemList[i].Id);
            if (gamerItem == null)
            {
                gamerItems.Add(new GamerItem(items.ItemList[i], useInitialSetting));
            }
            else
            {
                gamerItem.Item = items.ItemList[i];
            }
        }

        return gamerItems;
    }

    public static void SetGamerItems(List<GamerItem> currentItems)
    {
        string itemsString = JsonConvert.SerializeObject(new GamerItems {
            ItemList = currentItems
        });

        PlayerPrefs.SetString(itemsKey, itemsString);
    }
}