using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using UnityEngine;

public class Host : MonoBehaviour
{
    private static Host _singleton;
    public static Host Instance
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }
    private ushort port = 7777;
    private ushort maxClientCount = 1;
    [SerializeField] private GameObject playerPrefab;

    public GameObject PlayerPrefab => playerPrefab;

    public Server Server { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

#if UNITY_EDITOR
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
#else
            Console.Title = "Server";
            Console.Clear();
            Application.SetStackTraceLogType(UnityEngine.LogType.Log, StackTraceLogType.None);
            RiptideLogger.Initialize(Debug.Log, true);
#endif

        Debug.Log("HostAdalahCreate");
        Server = new Server();
        Server.ClientConnected += NewPlayerConnected;
        Server.ClientDisconnected += PlayerLeft;

        StartHost();
    }

    public void StartHost()
    {
        Server.Start(port, maxClientCount);
    }

    private void FixedUpdate()
    {
        Server.Tick();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();

        Server.ClientConnected -= NewPlayerConnected;
        Server.ClientDisconnected -= PlayerLeft;
    }

    private void NewPlayerConnected(object sender, ServerClientConnectedEventArgs e)
    {
        //foreach (Player player in Player.List.Values)
        //{
        //    if (player.Id != e.Client.Id)
        //        player.SendSpawn(e.Client.Id);
        //}
        Debug.Log("Client Connected. Yay!");
        UIManager.Instance.ClientHasConnected();

    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Debug.Log("Client Disconnected. Brp!");
        //Destroy(Player.List[e.Id].gameObject);
    }

    public void SendGameStartedDialog()
    {
        Message msg = Message.Create(MessageSendMode.reliable, Packets.ClientToHostId.gameStarted);
        Server.Send(msg, 1);
    }

    #region Special Packets

    #endregion
}
