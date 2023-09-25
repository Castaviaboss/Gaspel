using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    public string playerObj;
    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5, 5f));
        PhotonNetwork.Instantiate(playerObj, position, Quaternion.identity);
    }

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
        Debug.Log($"Player {newPlayer.NickName} enter  room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} left  room");
    }
}
