using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : ItemUsageController 
{
    [SerializeField] public Transform point1;
    [SerializeField] public Transform point2;

    private const int FRAMES_TO_WAIT = 2;

    private Dictionary<Character, int> currentCharactersFrames = new Dictionary<Character, int>();
    private List<Character> currentCharacters = new List<Character>();

    protected override void UseOverCharacter(Character character)
    {
        if (currentCharactersFrames.ContainsKey(character))
        {
            return;
        }

        float characterPositionX = character.transform.position.x;
        float distance1 = Mathf.Abs(point1.position.x - characterPositionX);
        float distance2 = Mathf.Abs(point2.position.x - characterPositionX);

        if (distance1 > distance2)
        {
            character.Move(point1.position.x);
        }
        else
        {
            character.Move(point2.position.x);
        }

        currentCharacters.Add(character);
        currentCharactersFrames[character] = FRAMES_TO_WAIT;
    }

    private void Update()
    {
        for(int i = currentCharacters.Count - 1; i >= 0; i--)
        {
            Character character = currentCharacters[i];
            currentCharactersFrames[character]--;
            if (currentCharactersFrames[character] == 0)
            {
                currentCharactersFrames.Remove(character);
                currentCharacters.Remove(character);
            }
        }
    }
}
