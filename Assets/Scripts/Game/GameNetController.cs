using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Game,
    Pause,
    Win,
}

public class GameNetController : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpawnZone spawnZone;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Spawner spawner;
    [SerializeField] private AbilityController abilityController;
    [SerializeField] private List<Player> _players = new List<Player>();

    #region Initialize

    public static GameNetController Controller;

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
        }
    }

    private void Start()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        Vector3 position = spawnZone.GetRandomZonePosition();
        var player = PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
        var playerComponent = player.GetComponent<BasePlayer>();
        playerComponent.InitializePlayer(50, 0);
    }
    
    #endregion

    public SpawnZone GetSpawnZone()
    {
        return spawnZone;
    }
    
    public ObjectPool GetObjectPool()
    {
        return objectPool;
    }
    
    public Spawner GetSpawner()
    {
        return spawner;
    }
    
    public AbilityController GetAbilityController()
    {
        return abilityController;
    }

    #region Network

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _players.Add(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _players.Remove(otherPlayer);
    }

    #endregion
}
