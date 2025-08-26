using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuState : IGameState
{
    private GameManager GM;

    public StartMenuState(GameManager gm)
    {
        GM = gm;
    }

    public void Enter()
    {
        UIManager.Instance.ShowStartMenu();
    }

    public void Update() { }

    public void Exit()
    {
    }
}
