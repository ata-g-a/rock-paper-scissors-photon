using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static bool IsPlayerConnected = false;
    public static string PlayerRoomName;

    public static UnityEvent ConnectedToServer = new UnityEvent();
    public static UnityEvent<bool, string> ConnectedToRoom = new UnityEvent<bool, string>();

    [SerializeField] PhotonView View;

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

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        //if we are master client then we should disable our own ui
        if (PhotonNetwork.IsMasterClient)
        {
            ConnectedToRoom?.Invoke(false, newPlayer.NickName);
        }
        else
        {
            ConnectedToRoom?.Invoke(true, newPlayer.NickName);
        }
        print("OnPlayerEnteredRoom " + PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ConnectedToRoom?.Invoke(true, "owner");
        }
        else
        {
            string otherPlayerName = "abc";
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player != PhotonNetwork.LocalPlayer)
                {
                    otherPlayerName = player.NickName;
                }
            }
            ConnectedToRoom?.Invoke(false, otherPlayerName);
        }
        print("OnJoinedRoom");
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

}
