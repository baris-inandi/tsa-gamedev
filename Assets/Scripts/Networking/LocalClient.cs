
using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using UnityEngine;


public class LocalClient : MonoBehaviour
{
	private static LocalClient _singleton;
	public static LocalClient Instance
	{
		get => _singleton;
		private set
		{
			if (_singleton == null)
				_singleton = value;
			else if (_singleton != value)
			{
				Debug.Log($"{nameof(LocalClient)} instance already exists, destroying object!");
				Destroy(value);
			}
		}
	}

	private ushort port = 7777;

	public Client Client { get; private set; }

	private void OnEnable()
	{
		Debug.Log("Enablenya");
		Instance = this;
		Client = new Client();
		Client.Connected += DidConnect;
		Client.ConnectionFailed += FailedToConnect;
		Client.ClientDisconnected += PlayerLeft;
		Client.Disconnected += DidDisconnect;
	}

	private void FixedUpdate()
	{
		Client.Tick();
	}

	private void OnApplicationQuit()
	{
		Client.Disconnect();

		Client.Connected -= DidConnect;
		Client.ConnectionFailed -= FailedToConnect;
		Client.ClientDisconnected -= PlayerLeft;
		Client.Disconnected -= DidDisconnect;
	}

	public void Connect(string _ip)
	{
		Client.Connect($"{_ip}:{port}");
	}

	private void DidConnect(object sender, EventArgs e)
	{
		Debug.Log("Bang, Did Connect.");
		UIManager.Instance.LocalClientSelfConnected();
	}

	private void FailedToConnect(object sender, EventArgs e)
	{
		Debug.Log("Failed To Connect");
		UIManager.Instance.BackToMain();
	}

	private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
	{
		//Destroy(Player.list[e.Id].gameObject);
	}

	private void DidDisconnect(object sender, EventArgs e)
	{
		Debug.Log("Disconnected");
		UIManager.Instance.BackToMain();
	}
}
