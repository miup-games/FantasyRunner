using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class CharacterRepository
{
    private static Characters _characters;

    public static Characters GetItems()
    {
        if (_characters != null)
        {
            return _characters;
        }
        string json = ((UnityEngine.TextAsset) UnityEngine.Resources.Load("Data/Characters")).text;
        try {
            _characters = JsonConvert.DeserializeObject<Characters>(json);
            return _characters;
        }
        catch(System.Exception e) {
            throw new System.Exception("Error creating Items: " + e.Message);
        }
    }

    public static Character GetCharacterByName(string characterName)
    {
        Characters characters = GetItems();
        for(int i = 0; i < characters.CharacterList.Length; i++)
        {
            if (characters.CharacterList[i].Name == characterName)
            {
                return characters.CharacterList[i];
            }
        }

        return null;
    }

    public static Character GetPlayer()
    {
        Characters characters = GetItems();
        for(int i = 0; i < characters.CharacterList.Length; i++)
        {
            if (characters.CharacterList[i].Id == CharacterConstants.PLAYER_ID)
            {
                return characters.CharacterList[i];
            }
        }

        return null;
    }
}