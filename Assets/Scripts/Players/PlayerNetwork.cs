using System;
using Photon.Pun;
using UnityEngine;

public class PlayerNetwork : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private MonoBehaviour[] components;
    
    private Vector3 _latestPosition;
    private BasePlayer _player;
    private int _lastCountOfHealth;
    private int _lastCountOfCoins;

    public int GetLastCountOfCoins()
    {
        return _lastCountOfCoins;
    }

    public BasePlayer GetPlayer()
    {
        return _player;
    }

    private void Start()
    {
        if (!photonView.IsMine)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false;
            }
        }
        
        _player = GetComponent<BasePlayer>();
        GameNetController.Instance.OnPlayerPrefabCreated(this);
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, _latestPosition, Time.deltaTime * 6f);
            _player.SetHealth(_lastCountOfHealth);
            _player.SetCoinsCount(_lastCountOfCoins);
        }
    }

    private void LateUpdate()
    {
        _latestPosition = transform.position;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext((int)_player.GetHealth());
            stream.SendNext((int)_player.GetCoinsCount());
        }
        else if(stream.IsReading)
        {
            _latestPosition = (Vector3)stream.ReceiveNext();
            _lastCountOfHealth = (int)stream.ReceiveNext();
            _lastCountOfCoins = (int)stream.ReceiveNext();
        }
    }
}
