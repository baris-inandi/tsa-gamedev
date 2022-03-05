using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField]
    private TMP_InputField IPField;

    [Header("Main Objects")]
    [SerializeField]
    private GameObject ClientJoin;
    [SerializeField]
    private GameObject HostWait;
    [SerializeField]
    private GameObject WelcomePanel;

    [Header("Child Objects")]
    [SerializeField]
    private TMP_Text HostWaitingText;
    [SerializeField]
    private TMP_Text HostWaitingIPText;
    [SerializeField]
    private TMP_Text ClientWaitingText;
    [SerializeField]
    private Button StartGameButton;


    private static UIManager _singleton;
    public static UIManager Instance
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

    public void Awake()
    {
        Instance = this;
    }

    public void HostGame()
    {
        PeerType.type = PeerType.GamePeerType.host;
        PeerType.otherType = PeerType.GamePeerType.client;
        gameObject.AddComponent<Host>();
        WelcomePanel.SetActive(false);
        HostWait.SetActive(true);
        HostWaitingIPText.text = $"Gamecode Is: {ZgUtils.GenerateGameCode.GenerateGameCode.Generate("132.98.1.4")}";
    }

    public void JoinGame()
    {
        PeerType.type = PeerType.GamePeerType.client;
        PeerType.otherType = PeerType.GamePeerType.host;
        gameObject.AddComponent<LocalClient>();
        Debug.Log("ChooseAdalah?");
        if (IPField.text == string.Empty)
            LocalClient.Instance.Connect("127.0.0.1");
        else
            LocalClient.Instance.Connect(ZgUtils.GenerateGameCode.GenerateGameCode.DecodeToIP(IPField.text));
        WelcomePanel.SetActive(false);
        ClientJoin.SetActive(true);
    }

    public void ClientHasConnected()
    {
        HostWaitingText.text = "Player 2 Has Joined. Game Now Can Be Started.";
        StartGameButton.interactable = true;
    }

    public void StartGame()
    {
        Handler.Instance.StartGame();
    }

    public void LocalClientSelfConnected()
    {
        ClientWaitingText.text = "Joined Game. Waiting For Host..";
    }

    public void BackToMain()
    {
        if (PeerType.type == PeerType.GamePeerType.client)
        {
            ClientJoin.SetActive(false);
            WelcomePanel.SetActive(true);
        }
        else
        {
            HostWait.SetActive(false);
            WelcomePanel.SetActive(true);
        }
    }
}
