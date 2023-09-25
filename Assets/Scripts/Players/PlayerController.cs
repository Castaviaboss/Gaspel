using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button fireButton;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector2 _direction;
    public Action onFire;

    private void Start()
    {
        joystick = GameController.Controller.GetJoystick();
        fireButton = GameController.Controller.GetFireButton();
        photonView = GetComponent<PhotonView>();
        fireButton.onClick.AddListener(FireAction);
    }

    private void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected) return;
        Movement();
    }

    private void Movement()
    {
        _horizontalInput = joystick.Horizontal;
        _verticalInput = joystick.Vertical;
        
        /*_horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");*/

        _direction = new Vector2(_horizontalInput, _verticalInput).normalized;

        if (_direction != Vector2.zero)
        {
            transform.Translate(_direction * moveSpeed * Time.deltaTime);
        }
    }

    public Vector2 GetDirection()
    {
        return _direction;
    }

    private void FireAction()
    {
        if(!photonView.IsMine) return;
        onFire?.Invoke();
    }
}
