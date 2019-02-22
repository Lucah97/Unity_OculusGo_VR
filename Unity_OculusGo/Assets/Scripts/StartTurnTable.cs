using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnTable : MonoBehaviour, IObjInteractionTarget {

    public Transform Obj;
    public Transform Where;

    public float LerpSpeed = 1;

    public GameObject HereButton;
    public GameObject BackButton;

    private Transform LerpThis;
    private Transform LerpHere;

    private Vector3 OrigObjPos;
    private Quaternion OrigObjRot;

    private Vector3 OrigTurn3DPos;
    private Quaternion OrigTurn3DRot;

    private bool Lerp = false;
    private float LerpDist;
    private float curDist;
    private float LerpVal = 0;
    private bool returning;

	// Use this for initialization
	void Start () {
        LerpDist = Vector3.Distance(Obj.position, Where.position);
        OrigObjPos = Obj.position;
        OrigObjRot = Obj.rotation;
    }
	
	// Update is called once per frame
	void Update () {
		if(Lerp)
        {
            curDist = Vector3.Distance(LerpThis.position, LerpHere.position);
            if ((LerpDist/2) < curDist)
            {
                LerpVal += Time.deltaTime*LerpSpeed;
            }
            else 
            {
                if (LerpVal >= 0.25f)
                {
                    LerpVal -= Time.deltaTime*LerpSpeed;
                }
            }

            LerpThis.position = Vector3.Lerp(LerpThis.position, LerpHere.position, LerpVal);

            if(curDist <= 0.02)
            {
                Debug.Log("I did the Lerp m8");
                LerpThis.position = LerpHere.position;
                if (!returning)
                {
                    LerpThis.parent = LerpHere;
                    LerpThis.localPosition = new Vector3(0, 0, 0);
                    LerpHere.GetComponent<Rotate3D>().enabled = true;
                    HereButton.SetActive(false);
                    BackButton.SetActive(true);
                }
                else
                {
                    Where.position = OrigTurn3DPos;
                    Where.rotation = OrigTurn3DRot;
                    Where.GetComponent<Rotate3D>().enabled = false;
                    BackButton.SetActive(false);
                    HereButton.SetActive(true);
                }

                LerpVal = 0;
                Lerp = false;
            }
        }
	}

    public void targetInteract(int v)
    {
        if (v == 0)
        {
            startLerp();
        }
        else
        {
            reverseLerp();
        }
    }

    public void startLerp()
    {
        OrigObjPos = Obj.position;
        OrigObjRot = Obj.rotation;
        Debug.Log("COME HERE");
        LerpThis = Obj;
        LerpHere = Where;
        returning = false;
        Lerp = true;
    }

    public void reverseLerp()
    {
        OrigTurn3DPos = Where.position;
        OrigTurn3DRot = Where.rotation;
        Debug.Log("PISS OFF");
        LerpThis.parent = LerpThis.parent.parent.GetChild(1).GetChild(0);
        LerpThis = Obj;
        LerpHere.position = OrigObjPos;
        LerpHere.rotation = OrigObjRot;
        returning = true;
        Lerp = true;
    }

}
