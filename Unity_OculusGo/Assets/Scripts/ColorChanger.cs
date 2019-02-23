using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour, IObjInteractionTarget
{
    public DescriptionHandler DH;

    private GameObject ColorThis;
    private Material ObjMat;

    public int ObjToColor;

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
            SetObjColor();
        }
    }

    private void Update()
    {
        if (UnityHasNoChill)
        {
            if(DH.curObj != null)
            {
                SetObjColor();
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
                SetObjColor();
                break;
            case 2:
                CurCol = color2;
                SetObjColor();
                break;
            case 3:
                CurCol = color3;
                SetObjColor();
                break;
        }
    }

    public void SetObjColor()
    {
        for (int i = 0; i < ObjToColor; i++)
        {
            ColorThis = DH.curObj.transform.GetChild(i).gameObject;
            ObjMat = ColorThis.GetComponent<Renderer>().material;
            ObjMat.color = CurCol;
            //To get the rest of the Arm Colored
            if(ColorThis.name == "lever2")
            {
                ColorThis = ColorThis.transform.GetChild(0).gameObject;
                ObjMat = ColorThis.GetComponent<Renderer>().material;
                ObjMat.color = CurCol;

                //To get the Ladle Colored
                if (ColorThis.name == "lever1")
                {
                    ColorThis = ColorThis.transform.GetChild(0).gameObject;
                    ObjMat = ColorThis.GetComponent<Renderer>().material;
                    ObjMat.color = CurCol;
                }
            }
        }
    }

}
