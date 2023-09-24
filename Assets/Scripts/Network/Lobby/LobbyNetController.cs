using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyNetController : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void CreateRoomAction(string roomName, string nickName)
    {
        SetUsersSettings(nickName);
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoomAction(string roomName, string nickName)
    {
        SetUsersSettings(nickName);
        PhotonNetwork.JoinRoom(roomName);
    }

    private void SetUsersSettings(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"user {PhotonNetwork.NickName} is joined");
        PhotonNetwork.LoadLevel("Game");
    }
}
