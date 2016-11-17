using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class StageRepository
{
    public static Stages GetStages()
    {
        string json = ((UnityEngine.TextAsset) UnityEngine.Resources.Load("Data/Stages")).text;
        try {
            return JsonConvert.DeserializeObject<Stages>(json);
        }
        catch(System.Exception e) {
            throw new System.Exception("Error creating Stages: " + e.Message);
        }
    }
}