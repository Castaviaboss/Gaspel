using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private GameNetController netController;
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
        gameMenu.SetActive(!gameMenu.activeSelf);
    }

    private void ShowPopupMenu()
    {
        gameMenu.SetActive(false);
        gamePopup.SetActive(!gamePopup.activeSelf);
    }
}
