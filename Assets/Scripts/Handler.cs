using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{

    #region Singleton and Attributes
    [SerializeField]
    private GameObject HostPlayerPrefab;
    [SerializeField]
    private GameObject ClientPlayerPrefab;

    [SerializeField]
    private Material HostMaterial;
    [SerializeField]
    private GameObject ClientMaterial;


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
    #endregion

    #region Player Spawning
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
        GameObject MainPlayer;
        GameObject OtherPlayer;
        if (PeerType.type == PeerType.GamePeerType.host)
        {
			GameObject tempObject = GameObject.Find("HostPlayer_other");
			Vector3 tempVec = tempObject.transform.position;
            Quaternion q = tempObject.transform.rotation;			
            Destroy(tempObject);

			MainPlayer = Instantiate(HostPlayerPrefab, tempVec, q);
			MainPlayer.name = "HostPlayer";
			tempVec = Vector3.zero;
			q = Quaternion.identity;
			tempObject = null;


			players.Add(MainPlayer.AddComponent<Player>());
            MainPlayer.GetComponent<Player>().id = 0;
			Camera.main.GetComponent<CameraFollow>().target = MainPlayer.transform;

			OtherPlayer = GameObject.Find("ConnectedPlayer_other");
			OtherPlayer.name = "ConnectedPlayer";
            players.Add(OtherPlayer.AddComponent<Player>());
            OtherPlayer.GetComponent<Player>().id = 1;
        }
        else
        {
			OtherPlayer = GameObject.Find("HostPlayer_other");
			OtherPlayer.name = "HostPlayer";
            players.Add(OtherPlayer.AddComponent<Player>());
            OtherPlayer.GetComponent<Player>().id = 0;

            GameObject tempObject = GameObject.Find("ConnectedPlayer_other");
			Vector3 tempVec = tempObject.transform.position;
            Quaternion q = tempObject.transform.rotation;
			Destroy(tempObject);

			MainPlayer = Instantiate(ClientPlayerPrefab, tempVec, q);
			MainPlayer.name = "ConnectedPlayer";
			tempVec = Vector3.zero;
			q = Quaternion.identity;
			tempObject = null;

			players.Add(MainPlayer.AddComponent<Player>());
            MainPlayer.GetComponent<Player>().id = 1;
			Camera.main.GetComponent<CameraFollow>().target = MainPlayer.transform;
        }
    }
    #endregion

}
