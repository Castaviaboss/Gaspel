using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private TMP_InputField nickNameField;
    [SerializeField] private TMP_InputField createRoomField;
    [SerializeField] private TMP_InputField joinRoomField;
    [Header("Network")]
    [SerializeField] private LobbyNetController lobbyNetController;
    private void Awake()
    {
        createRoomButton.onClick.AddListener(CreateRoomButtonClicked);
        joinRoomButton.onClick.AddListener(JoinRoomButtonClicked);
        
        nickNameField.onValueChanged.AddListener(value => ControlButtonInteractable());
        joinRoomField.onValueChanged.AddListener(value => ControlButtonInteractable());
        createRoomField.onValueChanged.AddListener(value => ControlButtonInteractable());
        
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    private void ControlButtonInteractable()
    {
        bool isNickNameNotEmpty = !string.IsNullOrEmpty(GetFieldValue(nickNameField));
        bool isJoinRoomFieldNotEmpty = !string.IsNullOrEmpty(GetFieldValue(joinRoomField));
        bool isCreateRoomFieldNotEmpty = !string.IsNullOrEmpty(GetFieldValue(createRoomField));

        createRoomButton.interactable = isNickNameNotEmpty && isCreateRoomFieldNotEmpty;
        joinRoomButton.interactable = isNickNameNotEmpty && isJoinRoomFieldNotEmpty;
    }
    
    private string GetFieldValue(TMP_InputField field)
    {
        return field.text;
    } 
    
    private void CreateRoomButtonClicked()
    {
        lobbyNetController.CreateRoomAction(GetFieldValue(createRoomField), GetFieldValue(nickNameField));
    }

    private void JoinRoomButtonClicked()
    {
        lobbyNetController.JoinRoomAction(GetFieldValue(joinRoomField), GetFieldValue(nickNameField));
    }
}
