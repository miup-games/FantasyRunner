﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterConstants
{
    public enum CharacterType 
    {
        Character,
        Player,
        Enemy
    };

    public enum CharacterState
    {
        None,
        Attacking,
        Running,
        Dead,
        Win
    }

    public enum AttributeType 
    {
        Speed,
        Attack,
        Defense,
        BattleStageSpeed
    };

    public enum AttributeModifierType 
    {
        Additive,
        Multiply
    };

    public const int PLAYER_ID = 1;
    public const float DESTROY_DELAY_AFTER_DEAD = 3f;
    public const float DISAPEAR_DURATION = 1f;
}