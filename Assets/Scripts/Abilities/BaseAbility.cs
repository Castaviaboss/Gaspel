using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    private float _cachedLifeTime;
    public float lifeTime;
    public byte damage;
    protected BasePlayer InvokerPlayer;
    
    public virtual void InitializeAbility(BasePlayer player)
    {
        InvokerPlayer = player;
        _cachedLifeTime = lifeTime;
    }

    public virtual void Updatable() { }
    
    public void ResetAbilityValues()
    {
        lifeTime = _cachedLifeTime;
    }
}
