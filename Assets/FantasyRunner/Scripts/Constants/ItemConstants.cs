using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemConstants
{
    //This is just for 3D bone. We are using different weapons with different rotations and size,
    //so we need different bones to handle them.
    public enum WeaponType
    {
        SwordWide,
        Mace
    }

    public const float DISAPEAR_DURATION = 0.6f;

    public const float POWER_ITEM_DELAY = 7f;
    public const float POWER_ITEM_PBB = 1f;

    public const float POWER_DURATION = 8f;
    public const float POWER_GAME_SPEED = 5f;
    public const float POWER_PLAYER_SPEED_FACTOR = 2f;
    public const float POWER_MUSIC_SPEED = 1.8f;
}