using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class BasePlayer : MonoBehaviourPun
{
    [Header("Links")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TMP_Text healthView;
    [SerializeField] private TMP_Text coinView;
    private AbilityController _controller;
    private PhotonView _photonView;
    [Header("fields")]
    [SerializeField] private int health;
    [SerializeField] private int coinCount;
    
    private void Start()
    {
        healthView.text = health.ToString();
        
        _controller = GameController.Controller.GetAbilityController();
        
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

    public virtual void SetHealth(int value)
    {
        health = value;
    }

    public virtual void Attack()
    {
        _controller.UseAbilityForType(this);
    }
    
    public virtual void Death()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void TakeCoin(int value)
    {
        coinCount += value;
        coinView.text = coinCount.ToString();
    }

    public virtual int GetCoinsCount()
    {
        return coinCount;
    }

    public virtual void SetCoinsCount(int value)
    {
        coinCount = value;
    }
    
    private void OnDestroy()
    {
        playerController.onFire -= Attack;
    }
}
