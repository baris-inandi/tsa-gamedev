using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;

public class Player : MonoBehaviour
{
	public ushort id { get; set; }
	public ushort hp { get; set; }

	public void SendHP()
	{
		Message msg = Message.Create(MessageSendMode.reliable, Packets.ClientToHostId.SendHP);
	}
}
