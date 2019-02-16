using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateInputHandler : MonoBehaviour {

    [Header("Mouse Properties")]
    public Vector2 mouseSensitivity = new Vector2(1, 1);
    public bool lockMouse = true;

    private Vector3 curEulers = Vector3.zero;


    private void Start()
    {
        curEulers = transform.rotation.eulerAngles;
        applyCursorState();
    }

    void Update () {
		//Web / Mouse Input
        if (InputManager.instance.curPlatform == Platform.PL_Web)
        {
            Vector2 relMouseMovement = new Vector2(Input.GetAxis("Mouse X"),
                                                   Input.GetAxis("Mouse Y"));
            relMouseMovement *= mouseSensitivity;
            curEulers += new Vector3(-relMouseMovement.y, relMouseMovement.x, 0);

            //Restrict Vertical Angle
            curEulers.x = Mathf.Clamp(curEulers.x, -90f, 90f);

            transform.rotation = Quaternion.Euler(curEulers);
        }

        //Lock / Unlock Mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockMouse = !lockMouse;
            applyCursorState();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!lockMouse)
            {
                lockMouse = true;
                applyCursorState();
            }
        }

	}

    private void applyCursorState()
    {
        Cursor.lockState = (lockMouse) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = (lockMouse) ? false : true;
    }
}
