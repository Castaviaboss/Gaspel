using UnityEngine;
using UnityEngine.UI;


public class GameInstance : MonoBehaviour
{
    [SerializeField] private SpawnZone spawnZone;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Spawner spawner;
    [SerializeField] private AbilityController abilityController;
    [Header("Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button fireButton;
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    #region Initialize

    public static GameInstance Controller;

    private void Awake()
    {
        if (Controller == null)
        {
            Controller = this;
        }
    }
    
    #endregion

    public Camera GetCamera()
    {
        return _camera;
    }
    
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
