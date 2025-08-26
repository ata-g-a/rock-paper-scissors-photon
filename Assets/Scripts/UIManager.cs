using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Global")]
    [SerializeField] GameObject startMenuPanel;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text WinText;
    [SerializeField] TMP_Text DrawText;
    [SerializeField] TMP_Text LossText;
    [Space]
    [Space]
    [Header("Choices")]
    [Space]
    [SerializeField] Image playerChoiceImage;
    [SerializeField] Image computerChoiceImage;
    [SerializeField] CanvasGroup playerChoiceCanvasGroup;
    [SerializeField] CanvasGroup computerChoiceCanvasGroup;
    [Space]
    [Space]
    [Header("Cards")]
    [Space]
    [SerializeField] Image[] CardsSelected;
    [Space]
    [Space]
    [SerializeField] Sprite[] ItemsSprites;
    [Space]
    [Space]
    [Header("Leaderboard")]
    [Space]
    [SerializeField] GameObject LeaderboardResaultPrefab;
    [SerializeField] GameObject LeaderboardHolder;
    [Space]
    [Space]
    [Header("Multiplayer creating room")]
    [Space]
    [SerializeField] TMP_Text RoomId;
    [SerializeField] Button CreateRoomBtn;
    [SerializeField] Button JoinRoomBtn;
    [SerializeField] GameObject ConnectingToServerText;
    [Space]
    [Space]
    [Space]
    [Header("Multiplayer joining room")]
    [Space]
    [SerializeField] GameObject JoinRoomParent;
    [SerializeField] TMP_InputField JoinToRoomRoomIdInput;
    [SerializeField] Button JoinToRoomBtn;
    [SerializeField] Button JoinToRoomCancelBtn;
    [SerializeField] GameObject JoinToRoomConnectingToServerText;
    [Space]
    [Header("attachments")]
    [Space]
    [SerializeField] GameManager GM;

    void Awake()
    {
        Instance = this;
        NetworkManager.ConnectedToServer.AddListener(EnableRoomCreation);
        NetworkManager.ConnectedToRoom.AddListener(JoinedToRoom);
    }

    public void ShowStartMenu()
    {
        startMenuPanel.SetActive(true);
    }
    public void HideStartMenu()
    {
        SetPlayerChoice("Rock");
        playerChoiceCanvasGroup.alpha = 0f;
        computerChoiceCanvasGroup.alpha = 0f;
        startMenuPanel.SetActive(false);
    }

    public void ShowChoices(GameManager.Choice player, GameManager.Choice computer, int result)
    {
        print("call " + result);
        WinText.gameObject.SetActive(false);
        LossText.gameObject.SetActive(false);
        DrawText.gameObject.SetActive(false);
        if (result == 1)
        {
            WinText.gameObject.SetActive(true);
        }
        else if (result == 0)
        {
            DrawText.gameObject.SetActive(true);
        }
        else if (result == -1)
        {
            LossText.gameObject.SetActive(true);
        }

        if (GM.playerChoice == GameManager.Choice.Rock)
        {
            playerChoiceImage.sprite = ItemsSprites[0];
        }
        else if (GM.playerChoice == GameManager.Choice.Paper)
        {
            playerChoiceImage.sprite = ItemsSprites[1];
        }
        else if (GM.playerChoice == GameManager.Choice.Scissors)
        {
            playerChoiceImage.sprite = ItemsSprites[2];
        }

        if (GM.computerChoice == GameManager.Choice.Rock)
        {
            computerChoiceImage.sprite = ItemsSprites[0];
        }
        else if (GM.computerChoice == GameManager.Choice.Paper)
        {
            computerChoiceImage.sprite = ItemsSprites[1];
        }
        else if (GM.computerChoice == GameManager.Choice.Scissors)
        {
            computerChoiceImage.sprite = ItemsSprites[2];
        }

        StartCoroutine(ShowResualtCor());
        UpdateLeaderBoard(result);
    }

    IEnumerator ShowResualtCor()
    {
        playerChoiceCanvasGroup.DOFade(1f, 1f);
        computerChoiceCanvasGroup.DOFade(1f, 1f);
        yield return new WaitForSeconds(2f);
        startMenuPanel.SetActive(true);
    }

    void UpdateLeaderBoard(int result)
    {

        Transform resaultobj = Instantiate(LeaderboardResaultPrefab, LeaderboardHolder.transform).transform;

        resaultobj.GetChild(0).GetChild(0).GetComponent<Image>().sprite = playerChoiceImage.sprite;
        resaultobj.GetChild(1).GetChild(0).GetComponent<Image>().sprite = computerChoiceImage.sprite;

        if (result == 1)
        {
            resaultobj.GetComponent<Image>().color = Color.green;
            resaultobj.GetChild(2).GetComponent<TMP_Text>().text = "Win";
        }
        else if (result == 0)
        {
            resaultobj.GetComponent<Image>().color = Color.cyan;
            resaultobj.GetChild(2).GetComponent<TMP_Text>().text = "Draw";
        }
        else if (result == -1)
        {
            resaultobj.GetComponent<Image>().color = Color.red;
            resaultobj.GetChild(2).GetComponent<TMP_Text>().text = "Lost";
        }
    }

    public void HideChoices()
    {
        playerChoiceCanvasGroup.alpha = 0f;
        computerChoiceCanvasGroup.alpha = 0f;
    }

    public void ShowTimer(string time)
    {
        timerText.gameObject.SetActive(true);
        timerText.text = time;
    }

    public void HideTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        GM.ChangeState(GM.playingState);
        StartCoroutine(GM.playingState.StartTimerCor());
    }

    public void SetPlayerChoice(string choice)
    {
        for (int i = 0; i < CardsSelected.Length; i++)
        {
            CardsSelected[i].color = Color.white;
        }
        if (choice == "Rock")
        {
            GM.playerChoice = GameManager.Choice.Rock;
            CardsSelected[0].color = Color.cyan;
        }
        else if (choice == "Paper")
        {
            GM.playerChoice = GameManager.Choice.Paper;
            CardsSelected[1].color = Color.cyan;
        }
        else if (choice == "Scissors")
        {
            GM.playerChoice = GameManager.Choice.Scissors;
            CardsSelected[2].color = Color.cyan;
        }

    }

    public void UpdateRoomId(string roomid)
    {
        RoomId.text = roomid;
    }

    public void CreateRoom()
    {
        NetworkManager.CreateRoom();
    }

    public void JoinRoom()
    {
        NetworkManager.JoinRoom(JoinToRoomRoomIdInput.text);
        JoinToRoomBtn.interactable = false;
        JoinToRoomCancelBtn.interactable = false;
        JoinToRoomConnectingToServerText.SetActive(true);
    }

    void EnableRoomCreation()
    {
        CreateRoomBtn.interactable = true;
        JoinRoomBtn.interactable = true;
        ConnectingToServerText.SetActive(false);
    }

    void JoinedToRoom()
    {
        JoinRoomParent.SetActive(false);
    }

    void OnDestroy()
    {
        NetworkManager.ConnectedToServer.RemoveListener(EnableRoomCreation);
        NetworkManager.ConnectedToRoom.RemoveListener(JoinedToRoom);
    }
}
