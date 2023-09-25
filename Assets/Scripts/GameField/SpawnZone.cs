using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnZone : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer spawnZoneRenderer;
    private Vector3 _currentRandomPosition;
    
    public void SpawnObjectInZone(GameObject obj, Vector2 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
    
    public Vector3 GetRandomZonePosition()
    {
        Vector3 lossyScale = spawnZoneRenderer.transform.lossyScale;
        
        float scaledSpriteWidth = spawnZoneRenderer.sprite.bounds.size.x * lossyScale.x;
        float scaledSpriteHeight = spawnZoneRenderer.sprite.bounds.size.y * lossyScale.y;
        
        float randomX = Random.Range(transform.position.x - scaledSpriteWidth / 2f, transform.position.x + scaledSpriteWidth / 2f);
        float randomY = Random.Range(transform.position.y - scaledSpriteHeight / 2f, transform.position.y + scaledSpriteHeight / 2f);

        Vector3 spawnPosition = new Vector3(randomX, randomY);

        return spawnPosition;
    }
}
