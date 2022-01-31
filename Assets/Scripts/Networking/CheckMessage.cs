using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;

public class CheckMessage
{
    public static void CheckMsg(Message msg)
    {
        if (PeerType.type == PeerType.GamePeerType.host)
        {
            Host.Instance.Server.Send(msg, 1);
        }
        else
        {
            LocalClient.Instance.Client.Send(msg);
        }
    }
}
