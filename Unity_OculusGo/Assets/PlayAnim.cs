using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnim : MonoBehaviour, IObjInteractionTarget
{
    public GameObject AnimThis;
    private Animator ObjAnim;
    public float AnimLength = 4.05f;

    private bool timer = false;
    private float curTime = 0;

    // Use this for initialization
    void Start()
    {
        ObjAnim = AnimThis.GetComponent<Animator>();
        ObjAnim.enabled = false;
        timer = false;
        curTime = 0;
    }

    public void targetInteract(int v)
    {
        ObjAnim.enabled = true;
        timer = true;
    }

    public void stopAnim()
    {
        ObjAnim.enabled = false;
    }

    private void Update()
    {
        if (timer)
        {
            if(curTime <= AnimLength)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                timer = false;
                curTime = 0;
                stopAnim();
            }
        }
    }
}
