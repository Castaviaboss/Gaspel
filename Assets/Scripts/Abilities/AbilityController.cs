using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private ObjectPool _pool;
    [SerializeField] private List<BaseAbility> usesAbility;
    private void Start()
    {
        _pool = GameController.Controller.GetObjectPool();
    }

    public void UseAbilityForType(BasePlayer player)
    {
        var ability = _pool.GetInAbilityPool(PoolType.Ball);
        if(!ability) {Debug.LogWarning("ability is null"); return;}
        ability.InitializeAbility(player);
        usesAbility.Add(ability);
    }

    private void Update()
    {
        if (usesAbility.Count == 0) return;
        
        for (int i = 0; i < usesAbility.Count; i++)
        {
            var ability = usesAbility[i];
            if (ability.lifeTime >= 0f)
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
