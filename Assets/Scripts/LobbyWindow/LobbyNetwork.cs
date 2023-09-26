using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyNetwork : MonoBehaviourPunCallbacks
{
    #region Fields

    public Action<bool> onConnectedToRoom;
    public Action<Player, bool> onJoinToRoom;

    #endregion
    
    #region Initialize

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");
    }
    

    #endregion
    
    public void CreateRoomAction(string roomName, string nickName, int maxPlayers)
    {
        SetUsersSettings(nickName);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() {MaxPlayers = maxPlayers});
    }

    public void JoinRoomAction(string roomName, string nickName)
    {
        SetUsersSettings(nickName);
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        onConnectedToRoom?.Invoke(true);
        onJoinToRoom?.Invoke(PhotonNetwork.LocalPlayer, true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        onConnectedToRoom?.Invoke(false);
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        onJoinToRoom?.Invoke(newPlayer, true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        onJoinToRoom?.Invoke(otherPlayer, false);
    }

    public void LeaveRoomAction()
    {
        PhotonNetwork.LeaveRoom();
        onJoinToRoom?.Invoke(PhotonNetwork.LocalPlayer, false);
    }

    private void SetUsersSettings(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public void GameLoadingStart()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ReconnectAndRejoin();
    }
}
