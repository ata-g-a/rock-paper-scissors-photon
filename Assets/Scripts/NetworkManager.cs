using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static bool IsPlayerConnected = false;
    public static string PlayerRoomName;

    public static UnityEvent ConnectedToServer = new UnityEvent();
    public static UnityEvent ConnectedToRoom = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        ConnectedToServer?.Invoke();
    }

    public override void OnJoinedLobby()
    {
        IsPlayerConnected = true;
        PlayerRoomName = NameTheRoom();
        UIManager.Instance.UpdateRoomId(PlayerRoomName);
    }

    public override void OnJoinedRoom()
    {
        ConnectedToRoom?.Invoke();
    }

    public static void CreateRoom()
    {
        PhotonNetwork.CreateRoom(PlayerRoomName, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public static void JoinRoom(string roomid)
    {
        PhotonNetwork.JoinRoom(roomid);
    }

    string NameTheRoom()
    {
        return "RPS" + Random.Range(12345, 98765);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
