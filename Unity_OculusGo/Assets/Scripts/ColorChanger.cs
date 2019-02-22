using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour, IObjInteractionTarget
{
    public DescriptionHandler DH;

    public GameObject ColorThis;
    public Material ObjMat;
    public Color CurCol;

    public Color color1;
    public Color color2;
    public Color color3;

    private bool UnityHasNoChill = false;

    // Use this for initialization
    void Start()
    {
        if (DH.curObj == null)
        {
            UnityHasNoChill = true;
        }
        else
        {
            Debug.Log("Well Unity suddenly has some chill");
        }
    }

    private void Update()
    {
        if (UnityHasNoChill)
        {
            if(DH.curObj != null)
            {
                ColorThis = DH.curObj.transform.GetChild(2).gameObject;
                ObjMat = ColorThis.GetComponent<Renderer>().material;
                CurCol = ObjMat.color;
                UnityHasNoChill = false;
            }
        }
    }

    public void targetInteract(int v)
    {
        switch (v)
        {
            case 1:
                CurCol = color1;
                ObjMat.color = CurCol;
                break;
            case 2:
                CurCol = color2;
                ObjMat.color = CurCol;
                break;
            case 3:
                CurCol = color3;
                ObjMat.color = CurCol;
                break;
        }
    }

    public void SetObjColor()
    {
        ColorThis = DH.curObj.transform.GetChild(0).gameObject;
        ObjMat = ColorThis.GetComponent<Material>();
        ObjMat.color = CurCol;
    }

}
