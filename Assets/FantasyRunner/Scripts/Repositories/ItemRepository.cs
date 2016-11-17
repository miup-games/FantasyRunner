using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ItemRepository
{
    public static Items GetItems()
    {
        string json = ((UnityEngine.TextAsset) UnityEngine.Resources.Load("Data/Items")).text;
        try {
            return JsonConvert.DeserializeObject<Items>(json);
        }
        catch(System.Exception e) {
            throw new System.Exception("Error creating Items: " + e.Message);
        }
    }
}