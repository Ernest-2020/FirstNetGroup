using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonLogin : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _disconnectOrConnectFromServerButton;
    /// <summary>
    /// This client's version number. Users are separated from each other bygameVersion(which allows you to make breaking changes).
/// </summary>
    string gameVersion = "1";
    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master clientand all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initializationphase.
    /// </summary>
    void Start()
    {
        _disconnectOrConnectFromServerButton.onClick.AddListener(Connect);
        
    }
    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon CloudNetwork
    /// </summary>
    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiatethe connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. Ifit fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon OnlineServer.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log($"Successful connected to master! Region: {PhotonNetwork.CloudRegion}, ping: {PhotonNetwork.GetPing()}");
        ConnectButtonBehaivor();


    }
    private void ConnectButtonBehaivor()
    {
        _disconnectOrConnectFromServerButton.onClick.RemoveListener(Connect);
        _disconnectOrConnectFromServerButton.onClick.AddListener(() => PhotonNetwork.Disconnect());
        _disconnectOrConnectFromServerButton.image.color = Color.red;
        _disconnectOrConnectFromServerButton.GetComponentInChildren<Text>().text = "Disconnect Photon";
    }
    private void DisconnectButtonBehaivor()
    {
        _disconnectOrConnectFromServerButton.onClick.RemoveListener(() => PhotonNetwork.Disconnect());
        _disconnectOrConnectFromServerButton.onClick.AddListener(Connect);
        _disconnectOrConnectFromServerButton.image.color = Color.green;
        _disconnectOrConnectFromServerButton.GetComponentInChildren<Text>().text = "Connect Photon";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Disconnect Photon");
        DisconnectButtonBehaivor();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        _disconnectOrConnectFromServerButton.onClick.RemoveListener(() => PhotonNetwork.Disconnect());
    }
}
