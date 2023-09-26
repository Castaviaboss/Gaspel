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
        Vector3 zoneSize = GetZoneSize();
        
        float randomX = Random.Range(transform.position.x - zoneSize.x / 2f, transform.position.x + zoneSize.x / 2f);
        float randomY = Random.Range(transform.position.y - zoneSize.y / 2f, transform.position.y + zoneSize.y / 2f);

        Vector3 spawnPosition = new Vector3(randomX, randomY);

        return spawnPosition;
    }

    public Vector2 GetZoneSize()
    {
        Vector3 lossyScale = spawnZoneRenderer.transform.lossyScale;
        
        float scaledSpriteWidth = spawnZoneRenderer.sprite.bounds.size.x * lossyScale.x;
        float scaledSpriteHeight = spawnZoneRenderer.sprite.bounds.size.y * lossyScale.y;

        return new Vector2(scaledSpriteWidth, scaledSpriteHeight);
    }
}
