using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsOnlinePlaying = false;

    public enum Choice { Rock, Paper, Scissors }

    private IGameState currentState;

    public StartMenuState startMenuState;
    public PlayingRoundState playingState;
    public ShowingResultState showingState;

    public Choice playerChoice = Choice.Rock;
    public Choice computerChoice = Choice.Rock;

    void Start()
    {
        startMenuState = new StartMenuState(this);
        playingState = new PlayingRoundState(this);
        showingState = new ShowingResultState(this);

        ChangeState(startMenuState);
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
