using System;
using Photon.Pun;
using UnityEngine;

public enum PoolType
{
    Coin,
    Ball,
}

public enum AbilityPoolType
{
    Ball,
}
public class ObjectPool : MonoBehaviourPunCallbacks
{
    [SerializeField] private PoolObject[] poolObjects;
    [SerializeField] private PoolObjectAbility[] abilityPool;
    [SerializeField] private byte additionalCountForResize;

    private void Awake()
    {
        InitializePool();
        InitializeGenericPool();
    }

    #region Standart Pool

    private void InitializePool()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            var poolObj = poolObjects[i];
            poolObj.objectsInPool = new GameObject[poolObj.countObjectsInPool];
            for (int j = 0; j < poolObj.countObjectsInPool; j++)
            {
                var instantiatedObj = Instantiate(poolObj.objectPrefab, poolObj.parentTransform);
                poolObj.objectsInPool[j] = instantiatedObj;
                instantiatedObj.SetActive(false);
            }
        }
    }

    public GameObject GetInPool(PoolType type)
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            var poolObj = poolObjects[i];
            if(poolObj.type != type) continue;
            for (int j = 0; j < poolObj.objectsInPool.Length; j++)
            {
                var obj = poolObj.objectsInPool[j];
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
        }
        return ResizePool(type);
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private GameObject ResizePool(PoolType type)
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            var poolObj = poolObjects[i];
            if (poolObj.type != type) continue;
            
            poolObj.countObjectsInPool += additionalCountForResize;
            
            GameObject[] tempArray = new GameObject[poolObj.countObjectsInPool];
            
            for (int j = 0; j < poolObj.objectsInPool.Length; j++)
            {
                tempArray[j] = poolObj.objectsInPool[j];
            }
            
            for (int j = poolObj.objectsInPool.Length; j < poolObj.countObjectsInPool; j++)
            {
                GameObject additionalObj = Instantiate(poolObj.objectPrefab, poolObj.parentTransform);
                additionalObj.SetActive(false);
                tempArray[j] = additionalObj;
            }
            
            poolObj.objectsInPool = tempArray;
            return poolObj.objectsInPool[^1];
        }

        return null;
    }

    #endregion

    #region Ability Pool

    private void InitializeGenericPool()
    {
        for (int i = 0; i < abilityPool.Length; i++)
        {
            var poolObj = abilityPool[i];
            poolObj.objectsInPool = new BaseAbility[poolObj.countObjectsInPool];
            for (int j = 0; j < poolObj.countObjectsInPool; j++)
            {
                var instantiatedObj = Instantiate(poolObj.objectPrefab, poolObj.parentTransform);
                poolObj.objectsInPool[j] = instantiatedObj;
                instantiatedObj.gameObject.SetActive(false);
            }
        }
    }
    
    public BaseAbility GetInAbilityPool(PoolType type)
    {
        for (int i = 0; i < abilityPool.Length; i++)
        {
            var poolObj = abilityPool[i];
            if(poolObj.type != type) continue;
            for (int j = 0; j < poolObj.objectsInPool.Length; j++)
            {
                var obj = poolObj.objectsInPool[j];
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
        }
        Debug.Log("Ability pool size is limited");
        return null;
    }

    public void ReturnToPool(BaseAbility ability)
    {
        ability.ResetAbilityValues();
        ability.gameObject.SetActive(false);
    }

    #endregion
}

[Serializable]
public class PoolObject
{
    [Range(0, byte.MaxValue)]
    public byte countObjectsInPool;
    public PoolType type;
    public Transform parentTransform;
    public GameObject objectPrefab;
    public GameObject[] objectsInPool;
}

/*[Serializable]
public class PoolObjectComponent<T> where T : MonoBehaviour
{
    [Range(0, byte.MaxValue)]
    public byte countObjectsInPool;
    public PoolType type;
    public Transform parentTransform;
    public T objectPrefab;
    public T[] objectsInPool;
}*/
[Serializable]
public class PoolObjectAbility
{
    [Range(0, byte.MaxValue)]
    public byte countObjectsInPool;
    public PoolType type;
    public Transform parentTransform;
    public BaseAbility objectPrefab;
    public BaseAbility[] objectsInPool;
}