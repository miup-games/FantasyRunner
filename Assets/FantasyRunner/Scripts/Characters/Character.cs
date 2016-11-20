using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Characters
{
    public Character[] CharacterList;
}

public class Character : MonoBehaviour 
{
    public int Id { get; set; }
    public string PrefabName { get; set; }
    public string Name { get; set; }
    public CharacterConstants.CharacterType CharacterType { get; set; }
    public float AttackDelay { get; set; }
    public float MaxHp { get; set; }
    public float BaseAttack { get; set; }
    public float BaseDefenses { get; set; }
    public int DiePoints { get; set; }
}
