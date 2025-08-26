using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayingRoundState : MonoBehaviour, IGameState
{
    private GameManager GM;
    private int timer = 5;

    public PlayingRoundState(GameManager gm)
    {
        GM = gm;
    }

    public void Enter()
    {
        timer = 5;


        // انتخاب پیش‌فرض بازیکن
        GM.playerChoice = GameManager.Choice.Rock;

        // انتخاب کامپیوتر در شروع راند
        GM.computerChoice = (GameManager.Choice)Random.Range(0, 3);

        UIManager.Instance.ShowTimer(timer.ToString("F0"));
        UIManager.Instance.HideStartMenu();
    }

    public void Update()
    {
        if (timer <= 0f)
        {
            int result = GetResult(GM.playerChoice, GM.computerChoice);
            UIManager.Instance.ShowChoices(GM.playerChoice, GM.computerChoice, result);

            GM.ChangeState(GM.showingState);
        }
    }

    public void Exit()
    {
        UIManager.Instance.HideTimer();
    }

    public IEnumerator StartTimerCor()
    {
        timer = 5;
        WaitForSeconds WFS = new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);
        while (timer > 0)
        {
            UIManager.Instance.ShowTimer(timer.ToString("F0"));
            yield return WFS;
            timer--;
        }
        UIManager.Instance.HideTimer();
    }



    private int GetResult(GameManager.Choice player, GameManager.Choice computer)
    {
        if (player == computer) return 0;
        if ((player == GameManager.Choice.Rock && computer == GameManager.Choice.Scissors) ||
            (player == GameManager.Choice.Scissors && computer == GameManager.Choice.Paper) ||
            (player == GameManager.Choice.Paper && computer == GameManager.Choice.Rock))
            return 1;
        return -1;
    }
}
