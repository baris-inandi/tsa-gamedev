using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;


public class Packets
{
    internal enum HostToClientId : ushort
    {
        gameStarted = 1,
        playerMovement,
    }
    internal enum ClientToHostId : ushort
    {
        gameStarted = 1,
        playerMovement,
    }

    [MessageHandler((ushort)HostToClientId.playerMovement)]
    private static void ReceiveMovement(Message message)
    {
        Handler.Instance.players[(int)PeerType.otherType].transform.position = message.GetVector3();
        Handler.Instance.players[(int)PeerType.otherType].transform.rotation = message.GetQuaternion();
    }

    [MessageHandler((ushort)HostToClientId.playerMovement)]
    private static void ReceiveMovement2(ushort a, Message message)
    {
        Handler.Instance.players[(int)PeerType.otherType].transform.position = message.GetVector3();
        Handler.Instance.players[(int)PeerType.otherType].transform.rotation = message.GetQuaternion();
    }
}
