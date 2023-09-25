using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private GameNetController netController;
    [SerializeField] private GameObject gameMenuRoot;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gamePopup;
    [SerializeField] private Button leaveGameButton;

    private void Awake()
    {
        leaveGameButton.onClick.AddListener(netController.LeaveRoom);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowGameMenu();
        }
    }

    private void ShowGameMenu()
    {
        gameMenuRoot.SetActive(!gameMenuRoot.activeSelf);
        gameMenu.SetActive(!gameMenu.activeSelf);
        
    }
    
}
