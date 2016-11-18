using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class PlayerRepository
{
    private static string stageKey = "currentStage";
    private static string lastUnlockedStageKey = "lastUnlockedStage";
    private static string itemsKey = "items";
    private static string coinsKey = "coins";

    public static System.Action<int> OnCoinChange;

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

    public static void AddCoins(int deltaCoins)
    {
        int currentCoins = GetCoins();
        int coins = currentCoins + deltaCoins;
        PlayerPrefs.SetInt(coinsKey, coins);
        if (OnCoinChange != null)
        {
            OnCoinChange(coins);
        }
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(coinsKey);
    }

    public static List<GamerItem> GetGamerItems()
    {
        Items items = ItemRepository.GetItems();
        List<GamerItem> gamerItems;

        string itemsString = PlayerPrefs.GetString(itemsKey);

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

    public static List<Item> GetCurrentItems()
    {
        List<Item> currentItems = new List<Item>();
        List<GamerItem> allItems = GetGamerItems();

        for(int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].IsUsing)
            {
                currentItems.Add(allItems[i].Item);
            }
        }

        return currentItems;
    }

    public static void SetGamerItems(List<GamerItem> items)
    {
        string itemsString = JsonConvert.SerializeObject(new GamerItems {
            ItemList = items
        });

        PlayerPrefs.SetString(itemsKey, itemsString);
    }
}