using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public enum HostToClientId : ushort
{
	welcomePlayer = 1,
	playerMovement,
}
public enum ClientToHostId : ushort
{
	playerName = 1,
	playerInput,
}

public class PeerType
{
	public enum GamePeerType : ushort
	{
		host = 0,
		client = 1
	}

	public static GamePeerType type;

}

namespace IPGet
{
	public class GetIPV4
	{
		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					Debug.Log(ip);
					return ip.ToString();
				}
			}
			throw new System.Exception("No network adapters with an IPv4 address in the system!");
		}
	}
}
