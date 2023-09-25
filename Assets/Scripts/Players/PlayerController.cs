using System;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(!photonView.IsMine) return;
        
        if(Input.GetKey(KeyCode.A)) transform.Translate(-Time.deltaTime * 5f,0f,0f);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Time.deltaTime * 5f, 0f, 0f);
        if(Input.GetKey(KeyCode.W)) transform.Translate(0f,Time.deltaTime * 5f,0f);
        if(Input.GetKey(KeyCode.S)) transform.Translate(0f,-Time.deltaTime * 5f,0f);
    }
}
