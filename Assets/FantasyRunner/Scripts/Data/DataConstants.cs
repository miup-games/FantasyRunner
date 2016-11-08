using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataConstants
{
    #region Items
    private static Item hp = new Item
        {
            Id = 1,
            IconName = "ItemIcons/Hp",
            PrefabName = "ItemPrefabs/Hp",
            Delay = 3.5f,
            Cost = 2
        };

    private static Item speed = new Item
        {
            Id = 2,
            IconName = "ItemIcons/Speed",
            PrefabName = "ItemPrefabs/Speed",
            Delay = 3.5f,
            Cost = 2
        };

    private static Item swordWide = new Item
        {
            Id = 3,
            IconName = "ItemIcons/SwordWide",
            PrefabName = "ItemPrefabs/SwordWide",
            Delay = 3.5f,
            Cost = 3
        };

    private static Item mace = new Item
        {
            Id = 4,
            IconName = "ItemIcons/Mace",
            PrefabName = "ItemPrefabs/Mace",
            Delay = 3.5f,
            Cost = 2
        };

    private static Item dead = new Item
        {
            Id = 5,
            IconName = "ItemIcons/Dead",
            PrefabName = "ItemPrefabs/Dead",
            Delay = 3.5f,
            Cost = 10
        };

    private static Item teleport = new Item
        {
            Id = 6,
            IconName = "ItemIcons/Teleport",
            PrefabName = "ItemPrefabs/Teleport",
            Delay = 3.5f,
            Cost = 2
        };

    private static Item slim = new Item
        {
            Id = 7,
            IconName = "ItemIcons/SlimGreen",
            PrefabName = "ItemPrefabs/SlimGreen",
            Delay = 3.5f,
            Cost = 1
        };

    private static Item coin = new Item
        {
            Id = 8,
            IconName = "ItemIcons/Coin",
            PrefabName = "ItemPrefabs/Coin",
            Delay = 1.3f,
            Cost = 0
        };

    public static Item[] ITEMS =
        {
            coin.Clone(),
            slim.Clone(),
            mace.Clone(),
            swordWide.Clone(),
            speed.Clone(),
            hp.Clone()
        };
    #endregion

    #region Stage
    private static string mummy = "Enemies/Enemy_Mummy";
    private static string zombie = "Enemies/Enemy_Zombie";
    private static string troll = "Enemies/Enemy_Troll";
    private static string orc = "Enemies/Enemy_Orc";
    private static string orcRed = "Enemies/Enemy_OrcRed";


    public static List<List<StageEnemy>> GetEnemies()
    {
        float[] times = {4f, 2f, 1f, 0.5f};
        List<List<StageEnemy>> enemies = new List<List<StageEnemy>>();

        for(int i = 0; i < times.Length; i++)
        {
            List<StageEnemy> waveEnemies = new List<StageEnemy>();

            waveEnemies.Add(new StageEnemy(mummy, times[i]));
            waveEnemies.Add(new StageEnemy(zombie, times[i]));
            waveEnemies.Add(new StageEnemy(troll, times[i]));
            waveEnemies.Add(new StageEnemy(orc, times[i]));

            enemies.Add(waveEnemies);
        }

        List<StageEnemy> lastEnemies = new List<StageEnemy>();
        lastEnemies.Add(new StageEnemy(orcRed, 5f));
        enemies.Add(lastEnemies);

        return enemies;
    }
    #endregion
}