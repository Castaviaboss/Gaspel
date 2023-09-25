using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class CoinObject : MonoBehaviour
{
    [SerializeField] private byte coinCount = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<BasePlayer>();
        
        if (!player) return;
        player.TakeCoin(coinCount);
        this.gameObject.SetActive(false);
    }
}
