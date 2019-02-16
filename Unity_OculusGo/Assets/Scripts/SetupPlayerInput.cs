using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerInput : MonoBehaviour {

    public GameObject recticlePrefab;

    private void Start()
    {
        setupPlatformPlayer();
    }

    private void setupPlatformPlayer()
    {
        switch (InputManager.instance.curPlatform)
        {
            case (Platform.PL_Web):
                setupWebInput();
                break;

            case (Platform.PL_AnCa):
                setupAndroidCardboardInput();
                break;
        }
    }

    private void setupWebInput()
    {
        GetComponent<AlternateInputHandler>().enabled = true;
        GetComponent<OVRHeadsetEmulator>().enabled = false;
        GetComponent<OVRCameraRig>().enabled = false;
        GetComponent<OVRManager>().enabled = false;

        GetComponent<RayCastPointer>().oculusPointer.parent = this.transform;
        GetComponent<RayCastPointer>().setCursorScale(1.25f);
        GetComponent<RayCastPointer>().mouseMode = true;

        Camera newCam = gameObject.AddComponent<Camera>();
        newCam.fieldOfView = 70f;
        Camera.SetupCurrent(newCam);
    }

    private void setupAndroidCardboardInput()
    {
        GetComponent<RayCastPointer>().oculusPointer.position = this.transform.position;
        GetComponent<RayCastPointer>().oculusPointer.parent = this.transform;

        Camera newCam = gameObject.AddComponent<Camera>();
        newCam.tag = "MainCamera";
        Camera.SetupCurrent(newCam);

        GameObject rect = Instantiate(recticlePrefab);
        rect.transform.parent = newCam.transform;
        rect.transform.localPosition = Vector3.zero;

        GetComponent<GvrEditorEmulator>().enabled = true;
        GetComponent<GvrControllerInput>().enabled = true;

        GetComponent<RayCastPointer>().setCursorScale(1.25f);
        GetComponent<LineRenderer>().enabled = false;
    }
}
