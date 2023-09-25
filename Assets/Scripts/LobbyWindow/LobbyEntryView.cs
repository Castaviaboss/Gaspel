using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyEntryView: BaseUiObject
{
    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button joinRoomButton;
    [SerializeField] private TMP_InputField nickNameField;
    [SerializeField] private TMP_InputField createRoomField;
    [SerializeField] private TMP_InputField joinRoomField;

    public override void InitializeMenu()
    {
        base.InitializeMenu();
        createRoomButton.onClick.AddListener(CreateRoomButtonClicked);
        joinRoomButton.onClick.AddListener(JoinRoomButtonClicked);
        
        nickNameField.onValueChanged.AddListener(value => ControlButtonInteractable());
        joinRoomField.onValueChanged.AddListener(value => ControlButtonInteractable());
        createRoomField.onValueChanged.AddListener(value => ControlButtonInteractable());

        lobby.GetNetworkController().onConnectedToRoom += IsExecuteJoin;
        
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
        lobby.GetNetworkController().CreateRoomAction(GetFieldValue(createRoomField), GetFieldValue(nickNameField), lobby.GetMaxPlayerCount());
    }

    private void JoinRoomButtonClicked()
    {
        lobby.GetNetworkController().JoinRoomAction(GetFieldValue(joinRoomField), GetFieldValue(nickNameField));
    }

    private void IsExecuteJoin(bool value)
    {
        if (value)
        {
            lobby.ShowWindow(WindowsType.LobbyRoom);
            lobby.HideWindow(WindowsType.LobbyEntry);
        }
        else
        {
            CallMessage("The room doesn't exist");
        }
    }

    private void OnDestroy()
    {
        lobby.GetNetworkController().onConnectedToRoom -= IsExecuteJoin;
    }
}
