using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{

    public Vector2 InputVector { get; private set; }

    public Vector3 MousePosition { get; private set; }

    public bool GetJumpButtowDown { get; private set; }
	// Update is called once per frame
	void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
		GetJumpButtowDown = Input.GetKeyDown(KeyCode.Space);
		InputVector = new Vector2(h, v);

        MousePosition = Input.mousePosition;
    }
}
