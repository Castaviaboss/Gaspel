using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] private byte health;
    [SerializeField] private byte coinCount;

    public void InitializePlayer(byte health, byte coinCount)
    {
        this.health = health;
        this.coinCount = coinCount;
    }

    public virtual void GetDamage(byte value)
    {
        health -= value;
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void TakeCoin(byte value)
    {
        coinCount += value;
    }
}
