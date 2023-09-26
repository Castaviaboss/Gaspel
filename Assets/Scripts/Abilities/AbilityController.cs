using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AbilityController : MonoBehaviourPun
{
    private ObjectPool _pool;
    [SerializeField] private List<BaseAbility> usesAbility;
    private void Start()
    {
        _pool = GameInstance.Controller.GetObjectPool();
    }

    public void UseAbilityForType(BasePlayer player)
    {
        var id = GameNetController.Instance.GetNetPlayerByBasePlayer(player).photonView.ViewID;
        photonView.RPC("CallAbilityForClients", RpcTarget.All, id, player.GetDirection());
    }
    
    [PunRPC]
    public void CallAbilityForClients(int playerId, Vector2 direction)
    {
        var player = GameNetController.Instance.GetNetPlayerByPhotonView(playerId);
        var ability = _pool.GetInAbilityPool(PoolType.Ball);
        if(!ability) {Debug.LogWarning("ability is null"); return;}
        ability.InitializeAbility(player, direction);
        usesAbility.Add(ability);
    }

    private void Update()
    {
        if (usesAbility.Count == 0) return;
        
        for (int i = 0; i < usesAbility.Count; i++)
        {
            var ability = usesAbility[i];
            if (ability.lifeTime >= 0f && !ability.isExecute)
            {
                ability.Updatable();
                ability.lifeTime -= Time.deltaTime;
            }
            else
            {
                _pool.ReturnToPool(ability);
                usesAbility.Remove(ability);
            }
        }
    }
}
