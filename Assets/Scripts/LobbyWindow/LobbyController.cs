using System;
using UnityEngine;

public enum WindowsType
{
    LobbyEntry,
    LobbyRoom,
}

public class LobbyController : MonoBehaviour
{
    [Header("Menu Objects")] 
    [SerializeField] private GameObject lobbyEntry;
    [SerializeField] private GameObject lobbyRoom;
    [SerializeField] private LobbyNetwork lobbyNetController;
    [SerializeField] private byte maxPlayersInLobby = 10;

    public void ShowWindow(WindowsType type)
    {
        switch (type)
        {
            case WindowsType.LobbyEntry: lobbyEntry.SetActive(true); break;
            case WindowsType.LobbyRoom: lobbyRoom.SetActive(true); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void HideWindow(WindowsType type)
    {
        switch (type)
        {
            case WindowsType.LobbyEntry: lobbyEntry.SetActive(false); break;
            case WindowsType.LobbyRoom: lobbyRoom.SetActive(false); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public int GetMaxPlayerCount()
    {
        return maxPlayersInLobby;
    }
    public LobbyNetwork GetNetworkController()
    {
        return lobbyNetController;
    }
}
