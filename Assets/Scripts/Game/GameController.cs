using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Game,
    Pause,
    Win,
}
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnZone spawnZone;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Spawner spawner;
    [SerializeField] private AbilityController abilityController;
    [Header("Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button fireButton;

    #region Initialize

    public static GameController Controller;

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
        }
    }
    
    #endregion

    public SpawnZone GetSpawnZone()
    {
        return spawnZone;
    }
    
    public ObjectPool GetObjectPool()
    {
        return objectPool;
    }
    
    public Spawner GetSpawner()
    {
        return spawner;
    }
    
    public AbilityController GetAbilityController()
    {
        return abilityController;
    }

    public Joystick GetJoystick()
    {
        return joystick;
    }
    
    public Button GetFireButton()
    {
        return fireButton;
    }
}
