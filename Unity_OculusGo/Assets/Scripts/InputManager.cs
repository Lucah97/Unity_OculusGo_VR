using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public Platform curPlatform;

    private void Awake()
    {
        if (InputManager.instance == null)
        {
            InputManager.instance = this;
            InputManager.instance.curPlatform = determinePlatform();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private Platform determinePlatform()
    {
        Platform retVar = Platform.PL_OcGo;

        //Check Platform
        switch (Application.platform)
        {
            case (RuntimePlatform.Android):
                retVar = Platform.PL_AnCa;
                break;

            case (RuntimePlatform.IPhonePlayer):
                retVar = Platform.PL_AnCa;
                break;

            case (RuntimePlatform.WebGLPlayer):
                retVar = Platform.PL_Web;
                break;

            case (RuntimePlatform.WindowsPlayer):
                retVar = Platform.PL_Web;
                break;

            case (RuntimePlatform.WindowsEditor):
                retVar = Platform.PL_Web;
                break;
        }

        //Check for Oculus Go
        if (OVRPlugin.productName == "Oculus Go")
        {
            retVar = Platform.PL_OcGo;
        }

        Debug.Log("Currently running on Platform: " + retVar.ToString());

        return retVar;
    }
}

public enum Platform { PL_OcGo, PL_AnCa, PL_Web };