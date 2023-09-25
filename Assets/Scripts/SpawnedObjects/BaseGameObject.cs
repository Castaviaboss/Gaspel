using System;
using UnityEngine;


public class BaseGameObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
