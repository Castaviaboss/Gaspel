using Photon.Pun;
using Photon.Realtime;

public class LoadingNetController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Connect();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "1";
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ReconnectAndRejoin();
    }
}
