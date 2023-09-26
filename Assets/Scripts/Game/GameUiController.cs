using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gamePopup;
    [SerializeField] private Button leaveGameButton;
    [SerializeField] private Button leaveGameButtonPopup;
    [Header("Popup")] 
    [SerializeField] private TMP_Text winnerNickname;
    [SerializeField] private TMP_Text winnerScore;
    private GameNetController _networkController;

    private void Start()
    {
        _networkController = GameNetController.Instance;
        
        leaveGameButton.onClick.AddListener(_networkController.LeaveRoom);
        leaveGameButtonPopup.onClick.AddListener(_networkController.LeaveRoom);
        _networkController.onGameStateChanged += ShowPopupMenu;
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

    private void ShowPopupMenu(GameState state)
    {
        if(state != GameState.Win) return;

        winnerNickname.text = _networkController.GetChampion().photonView.Controller.NickName;
        winnerScore.text = _networkController.GetChampion().GetPlayer().GetCoinsCount().ToString();
        
        gameMenu.SetActive(false);
        gamePopup.SetActive(!gamePopup.activeSelf);
    }

    private void OnDestroy()
    {
        _networkController.onGameStateChanged -= ShowPopupMenu;
    }
}
