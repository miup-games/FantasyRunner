using System;
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
        Jumping
    }

    public enum AttributeType 
    {
        Speed,
        Attack
    };

    public enum AttributeModifierType 
    {
        Additive,
        Multiply
    };

    public const float DESTROY_DELAY_AFTER_DEAD = 3f;
    public const float DISAPEAR_DURATION = 1f;
}