using System;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    private SpawnZone _zone;
    private Camera _camera;
    
    private void Start()
    {
        _zone = GameInstance.Controller.GetSpawnZone();
        _camera = GameInstance.Controller.GetCamera();
    }
    
    private void Update()
    {
        Vector3 viewportPosition = _camera.WorldToViewportPoint(transform.position);
        
        if (viewportPosition.x < 0f || viewportPosition.x > 1f ||
            viewportPosition.y < 0f || viewportPosition.y > 1f)
        {
            TeleportToOppositeSide();
        }
    }

    private void TeleportToOppositeSide()
    {
        Vector3 currentPosition = transform.position;
        Vector3 viewportPosition = _camera.WorldToViewportPoint(currentPosition);
        
        if (viewportPosition.x < 0f)
        {
            currentPosition.x = _camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)).x;
        }
        else if (viewportPosition.x > 1f)
        {
            currentPosition.x = _camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).x;
        }

        if (viewportPosition.y < 0f)
        {
            currentPosition.y = _camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f)).y;
        }
        else if (viewportPosition.y > 1f)
        {
            currentPosition.y = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).y;
        }

        transform.position = currentPosition;
    }
}
