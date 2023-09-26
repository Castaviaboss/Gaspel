using Photon.Pun;
using UnityEngine;

public class BaseAbility : MonoBehaviourPun, IPunObservable
{
    private float _cachedLifeTime;
    public float lifeTime;
    public byte damage;
    public bool isExecute;
    protected BasePlayer InvokerPlayer;
    
    public virtual void InitializeAbility(BasePlayer player)
    {
        InvokerPlayer = player;
        _cachedLifeTime = lifeTime;
        isExecute = false;
    }
    
    public virtual void InitializeAbility(BasePlayer player, Vector2 direction)
    {
        InitializeAbility(player);
    }
    
    public virtual void Updatable() { }
    
    public void ResetAbilityValues()
    {
        lifeTime = _cachedLifeTime;
    }

    public virtual void AbilityExecute()
    {
        isExecute = true;
        ResetAbilityValues();
    }


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(lifeTime);
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading)
        {
            lifeTime = (float)stream.ReceiveNext();
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}
