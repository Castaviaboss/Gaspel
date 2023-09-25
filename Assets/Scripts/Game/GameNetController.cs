using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNetController : MonoBehaviourPunCallbacks
{
    public static GameNetController Instance;
    [SerializeField] private List<PlayerNetwork> playerList = new List<PlayerNetwork>();
    /*private PlayerNetwork _championPlayer;*/

    public List<PlayerNetwork> GetPlayersList()
    {
        return playerList;
    }
    
    #region Initialize

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    
    #endregion
    
    /*private void InitializePlayer()
    {
        Debug.Log("init");
        
        var player = PhotonNetwork.Instantiate("Player",GameController.Controller.GetSpawnZone().GetRandomZonePosition(), Quaternion.identity);
        var playerComponent = player.GetComponent<BasePlayer>();
        
        player.gameObject.name = PhotonNetwork.NickName;
        playerComponent.InitializePlayer(50, 0);
    }*/
    
    #region Network

    public void OnPlayerPrefabCreated()
    {
        playerList = FindObjectsOfType<PlayerNetwork>(true).ToList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerList = FindObjectsOfType<PlayerNetwork>(true).ToList();
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    
    #endregion
}
