using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    Test,
    Game,
    Win,
}
public class GameNetController : MonoBehaviourPunCallbacks
{
    public static GameNetController Instance;
    [SerializeField] private List<PlayerNetwork> playerList = new List<PlayerNetwork>();
    [SerializeField] private GameState gameState;
    public Action<GameState> onGameStateChanged;
    private PlayerNetwork _playerWinner;

    public List<PlayerNetwork> GetPlayersList()
    {
        return playerList;
    }

    public PlayerNetwork GetChampion()
    {
        return _playerWinner;
    }
    
    public PlayerNetwork GetNetPlayerByBasePlayer(BasePlayer player)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            var playerObj = playerList[i];
            if (player == playerObj.GetPlayer())
            {
                return playerObj;
            }
        }
        return null;
    }
    
    public BasePlayer GetNetPlayerByPhotonView(int id)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            var playerObj = playerList[i];
            if (id == playerObj.GetPlayer().photonView.ViewID)
            {
                return playerObj.GetPlayer();
            }
        }
        return null;
    }
    
    public PlayerNetwork GetNetPlayerByPhotonPlayer(Player player)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            var playerObj = playerList[i];
            if (player == playerObj.GetPlayer().photonView.Controller)
            {
                return playerObj;
            }
        }
        return null;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        onGameStateChanged?.Invoke(gameState);
    }
    
    #region Initialize

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        ChangeGameState(GameState.Test);
    }
    
    #endregion
    
    private void FixedUpdate()
    {
        CheckPlayersWinner();
    }

    private void CheckPlayersWinner()
    {
        if (gameState == GameState.Game)
        {
            int activePlayers = 0;
            for (var index = 0; index < playerList.Count; index++)
            {
                var player = playerList[index];
                activePlayers += player.gameObject.activeSelf ? 1 : 0;
            }
            if (activePlayers <= 1)
            {
                for (var index = 0; index < playerList.Count; index++)
                {
                    var player = playerList[index];
                    if (player.gameObject.activeSelf)
                    {
                        _playerWinner = player;
                        ChangeGameState(GameState.Win);
                    }
                    
                }
            }
        };
    }
    
    #region Network

    public void OnPlayerPrefabCreated(PlayerNetwork newPlayer)
    {
        playerList.Add(newPlayer);

        if (playerList.Count == 2)
        {
            ChangeGameState(GameState.Game);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerList.Remove(GetNetPlayerByPhotonPlayer(otherPlayer));

        if (playerList.Count <= 1)
        {
            ChangeGameState(GameState.Win);
        }
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    
    #endregion
}
