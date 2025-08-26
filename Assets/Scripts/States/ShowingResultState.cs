using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowingResultState : IGameState
{
    private GameManager GM;
    private float timer = 2f;

    public ShowingResultState(GameManager gm)
    {
        GM = gm;
    }

    public void Enter()
    {
    }

    public void Update()
    {

    }

    public void Exit() { }
}
