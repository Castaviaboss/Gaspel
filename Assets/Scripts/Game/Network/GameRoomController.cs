using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoomController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            SceneManager.LoadScene("Lobby");
            return;
        }
        
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties);
        
        GameObject playerPrefab = PhotonNetwork.Instantiate("Player", GameInstance.Controller.GetSpawnZone().GetRandomZonePosition(), Quaternion.identity, 0);
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
