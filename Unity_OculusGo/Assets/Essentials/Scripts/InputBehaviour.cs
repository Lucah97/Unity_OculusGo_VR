using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour {

    public Transform Player;
    public Transform Trigger;
    public Transform Triggered;
    public Transform Untriggered;

    public Transform Livingroom;
    public Transform Kitchen;
    public Transform Bedroom;

    public int CurPos;

    public GameObject LivingRoomSchalu;
    public GameObject LivingRoomSchalu_2;
    public GameObject LivingRoomSchalu_3;
    private bool open;

    public GameObject BedroomSchalu;
    public GameObject BedroomSchalu_2;
    public GameObject BedroomSchalu_3;
    private bool openBed;
    

        // Use this for initialization
        void Start () {
        CurPos = 0;
        open = false;
        openBed = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            if(CurPos == 0)
            {
                CurPos = 1;
                Player.position = Kitchen.position;
                Player.rotation = Kitchen.rotation;
            }
            else if(CurPos == 1)
            {
                CurPos = 2;
                Player.position = Bedroom.position;
                Player.rotation = Bedroom.rotation;
            }
            else
            {
                CurPos = 0;
                Player.position = Livingroom.position;
                Player.rotation = Livingroom.rotation;
            }
        }

        if(OVRInput.GetDown(OVRInput.Button.One)||Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("open/close_Schalusine");
            if(CurPos == 0)
            {
                if (!open)
                {
                    openAnim(LivingRoomSchalu);
                    openAnim(LivingRoomSchalu_2);
                    openAnim(LivingRoomSchalu_3);
                    open = true;
                }
                else
                {
                    closeAnim(LivingRoomSchalu);
                    closeAnim(LivingRoomSchalu_2);
                    closeAnim(LivingRoomSchalu_3);
                    open = false;
                }
            }

            if (CurPos == 1)
            { /*
                if (!open)
                {
                    openAnim(LivingRoomSchalu);
                    open = true;
                }
                else
                {
                    closeAnim(LivingRoomSchalu);
                    open = false;
                } */
            }

            if (CurPos == 2)
            {
                if (!openBed)
                {
                    openAnimBed(BedroomSchalu);
                    openAnimBed(BedroomSchalu_2);
                    openAnimBed(BedroomSchalu_3);
                    openBed = true;
                }
                else
                {
                    closeAnimBed(BedroomSchalu);
                    closeAnimBed(BedroomSchalu_2);
                    closeAnimBed(BedroomSchalu_3);
                    openBed = false;
                }
            }
        }

    }

    public void openAnim(GameObject AnimObj)
    {
        AnimObj.GetComponent<Animation>()["Opening"].speed = 1;
        AnimObj.GetComponent<Animation>().Play();
    }

    public void openAnimBed(GameObject AnimObj)
    {
        AnimObj.GetComponent<Animation>()["Take 001"].speed = 1;
        AnimObj.GetComponent<Animation>().Play();
    }

    public void closeAnim(GameObject AnimObj)
    {
        AnimObj.GetComponent<Animation>()["Opening"].speed = -1;
        AnimObj.GetComponent<Animation>().Play();
    }

    public void closeAnimBed(GameObject AnimObj)
    {
        AnimObj.GetComponent<Animation>()["Take 001"].speed = -1;
        AnimObj.GetComponent<Animation>().Play();
    }
}
