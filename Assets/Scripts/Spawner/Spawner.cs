using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte startCoinCount = 10;
    [SerializeField] private float spawnCoinsTimer = 7f;
    private SpawnZone _spawnZone;
    private ObjectPool _objectPool;

    #region Initialize

    private void Start()
    {
        _spawnZone = GameInstance.Controller.GetSpawnZone();
        _objectPool = GameInstance.Controller.GetObjectPool();
        
        if(!PhotonNetwork.IsMasterClient) return;
        InitializeMap();
    }
    
    private void InitializeMap()
    {
        CoinSpawn(startCoinCount);
    }
    
    #endregion
    
    private void CoinSpawn(int amount)
    {
        Vector3[] randomPositions = new Vector3[amount];
        for (int i = 0; i < amount; i++)
        {
            randomPositions[i] = _spawnZone.GetRandomZonePosition();
        }
        
        photonView.RPC("SpawnCoinsOnClients", RpcTarget.All, randomPositions);
    }

    [PunRPC]
    private void SpawnCoinsOnClients(Vector3[] spawnPositions)
    {
        int amount = spawnPositions.Length;
        for (int i = 0; i < amount; i++)
        {
            _spawnZone.SpawnObjectInZone(_objectPool.GetInPool(PoolType.Coin), spawnPositions[i], Quaternion.identity);
        }
    }
    
    private void Update()
    {
        if(!PhotonNetwork.IsMasterClient) return;
        spawnCoinsTimer -= Time.deltaTime;

        if (spawnCoinsTimer <= 0)
        {
            CoinSpawn((int)Random.Range(1f, 4f));
            spawnCoinsTimer = Random.Range(1f, 6f);
        }
    }
}
