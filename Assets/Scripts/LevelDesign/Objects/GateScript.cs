using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
	[SerializeField]
	private Question gateQuestion;

    public void TriggerEnter()
    {
		  Debug.Log("TriggerEnter");
	  }

    public void TriggerExit()
    {
		  Debug.Log("TriggerExit");
    }
}
