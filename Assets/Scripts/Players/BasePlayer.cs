using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BasePlayer : MonoBehaviourPunCallbacks
{
    [Header("Links")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text healthView;
    [SerializeField] private TMP_Text coinView;
    private AbilityController _controller;
    [Header("fields")]
    [SerializeField] private int health;
    [SerializeField] private byte coinCount;

    public void InitializePlayer(byte health, byte coinCount)
    {
        this.health = health;
        this.coinCount = coinCount;
        
        healthView.text = health.ToString();
        _controller = GameNetController.Controller.GetAbilityController();
        playerController.onFire += Attack;
    }

    public virtual void GetDamage(int value)
    {
        health -= value;
        healthView.text = health.ToString();
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual Vector2 GetDirection()
    {
        return playerController.GetDirection();
    }
    
    public virtual int GetHealth()
    {
        return health;
    }

    public virtual void Attack()
    {
        _controller.UseAbilityForType(this);
    }
    
    public virtual void Death()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void TakeCoin(byte value)
    {
        coinCount += value;
        coinView.text = coinCount.ToString();
    }
    
    private void OnDestroy()
    {
        playerController.onFire -= Attack;
    }
}
