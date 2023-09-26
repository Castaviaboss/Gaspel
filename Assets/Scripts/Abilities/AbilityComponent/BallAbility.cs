using UnityEngine;

public class BallAbility : BaseAbility
{
    [SerializeField] private Rigidbody2D rigidbody2DBall;
    [SerializeField] private float flySpeed;
    [SerializeField] private Vector2 direction;
    private readonly Vector2 _defaultDirection = Vector2.up;

    public override void InitializeAbility(BasePlayer player, Vector2 direction)
    {
        base.InitializeAbility(player, direction);
        if (direction is { x: 0f, y: 0f })
        {
            direction = _defaultDirection;
        }
        transform.position = player.transform.position;
        rigidbody2DBall.AddForce(direction * flySpeed, ForceMode2D.Impulse);
    }

    /*public override void InitializeAbility(BasePlayer player)
    {
        base.InitializeAbility(player);
        direction = InvokerPlayer.GetDirection();
        if (direction is { x: 0f, y: 0f })
        {
            direction = _defaultDirection;
        }
        transform.position = player.transform.position;
        rigidbody2DBall.AddForce(direction * flySpeed, ForceMode2D.Impulse);
    }*/
    
    public override void Updatable() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == null) return;
        if(other.gameObject == InvokerPlayer.gameObject) return;

        var enemyPlayer = other.gameObject.GetComponent<BasePlayer>();
        if (enemyPlayer)
        {
            enemyPlayer.GetDamage(damage);
            gameObject.SetActive(false);
            AbilityExecute();
        }
    }
}
