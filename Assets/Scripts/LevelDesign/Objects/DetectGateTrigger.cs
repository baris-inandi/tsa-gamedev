using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGateTrigger : MonoBehaviour
{
	GateScript gs;
	void Awake()
    {
		gs = transform.GetComponentInParent<GateScript>();
	}
    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Host"/*PeerType.MainPlayerTag*/))
		gs.TriggerEnter();
	}

    void OnTriggerExit(Collider coll)
    {
        if(coll.CompareTag("Host"/*PeerType.MainPlayerTag*/))
		gs.TriggerExit();
	}
}
