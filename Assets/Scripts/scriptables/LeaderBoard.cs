using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LeaderBoard", menuName = "ScriptableObjects/LeaderBoard")]
public class LeaderBoard : ScriptableObject
{

    public List<string> IsPlayerWon = new List<string>();

    public void SetWinner(string state)
    {
        IsPlayerWon.Add(state);
    }

}
