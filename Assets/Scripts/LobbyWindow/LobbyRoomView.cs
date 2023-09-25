using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyRoomView : BaseUiObject
{
    [SerializeField] private TMP_Text roomName;
    private List<Player> _playerList = new List<Player>();
    [SerializeField] private Button testAsSingle;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button leaveRoomButton;

    public override void InitializeMenu()
    {
        base.InitializeMenu();
        testAsSingle.onClick.AddListener(StartGame);
        startGameButton.onClick.AddListener(StartGame);
        leaveRoomButton.onClick.AddListener(LeaveTheRoom);

        lobby.GetNetworkController().onJoinToRoom += UpdatePlayerList;

        startGameButton.interactable = false;
    }
    
    private void StartGame()
    {
        lobby.GetNetworkController().GameLoadingStart();
    }

    private void LeaveTheRoom()
    {
        lobby.GetNetworkController().LeaveRoomAction();
        lobby.ShowWindow(WindowsType.LobbyEntry);
        lobby.HideWindow(WindowsType.LobbyRoom);
    }

    private void UpdatePlayerList(Player player, bool isEntry)
    {
        if (isEntry)
        {
            _playerList.Add(player);
            AddMessage($"{player.NickName} joined the game");
        }
        else
        {
            _playerList.Remove(player);
            AddMessage($"{player.NickName} leave the game");
        }
        
        if(!PhotonNetwork.IsMasterClient) return;
        startGameButton.interactable = _playerList.Count >= 2;
    }
}
