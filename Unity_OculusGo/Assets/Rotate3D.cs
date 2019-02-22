using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate3D : MonoBehaviour {

    private Vector2 TouchPadPos;

    private Vector3 EulerRotate;

	// Reset Values
	void Awake () {
        EulerRotate = new Vector3(0, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
	
	// Rotate by a third of the touchpad input
	void Update () {
        TouchPadPos = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

        EulerRotate.y = TouchPadPos.x / 3;
        EulerRotate.z = TouchPadPos.y / 3;

        transform.Rotate(EulerRotate);
    }
}
