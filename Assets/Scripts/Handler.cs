using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{

    [SerializeField]
    private GameObject LocalPlayerPrefab;
    [SerializeField]
    private GameObject OtherPlayerPrefab;

    public List<Player> players = new List<Player>(2);
    private static Handler _singleton;
    public static Handler Instance
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
    void Awake()
    {
		Instance = this;
        DontDestroyOnLoad(gameObject);
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Top Down Movement");
        Host.Instance.SendGameStartedDialog();
        StartCoroutine(WaitFrame());
	}
    IEnumerator WaitFrame()
    {
        yield return null;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject player;
        GameObject player2;
        if (PeerType.type == PeerType.GamePeerType.host)
        {
            player = GameObject.Find("HostPlayer");
            players.Add(player.AddComponent<Player>());
            player.transform.position = new Vector3(-25, 5, 25);
            player.GetComponent<Player>().id = 0;
            player2 = GameObject.Find("ConnectedPlayer");
            player2.transform.position = new Vector3(-25, 5, 35);
            players.Add(player2.AddComponent<Player>());
            player2.GetComponent<Player>().id = 1;
        }
        else
        {
            player2 = GameObject.Find("ConnectedPlayer");
            players.Add(player2.AddComponent<Player>());
            player2.transform.position = new Vector3(-25, 5, 25);
            player2.GetComponent<Player>().id = 0;
            player = GameObject.Find("HostPlayer");
            players.Add(player.AddComponent<Player>());
            player.GetComponent<Player>().id = 1;
            player.transform.position = new Vector3(-25, 5, 35);
        }

    }
}
