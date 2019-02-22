using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IObjInteractionTarget
{
    [Header("Teleport properties")]
    public Transform Teleportlocation;
    public Transform Player;
    public List<GameObject> Visible = new List<GameObject>();

    [Header("Fade Properties")]
    public Color fadeColor = Color.black;
    public float fadeInTime = 0.75f;
    public float fadeOutTime = 0.75f;

    private bool fadeDirection = true;
    private float elapsedTime = 0;
    private Renderer TeleportRenderer;

    private bool doTheFade = false;

    void OnEnable()
    {
        foreach (Renderer r in this.GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    void Start()
    {
        TeleportRenderer = Player.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
    }

    void Update()
    {
        if (doTheFade)
        {
            ProgressBarSetup.instance.setEnabled(false);
            beginFade();
        }
    }

    public void targetInteract(int v)
    {
        //Setup Renderer
        TeleportRenderer.enabled = true;
        TeleportRenderer.material.color = fadeColor;
        

        //Start Fading
        stopAllTheFade();
        doTheFade = true;


        
        //Delete all Amazon Echo Chat
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Echo"))
        {
           
            try
            {
                g.GetComponent<EchoInteraction>().deleteChat();
            }
            catch {}
        }
    }

    private void beginFade()
    {
        if ((elapsedTime > fadeOutTime) && (fadeDirection))
        {

            fadeDirection = false;
            elapsedTime = fadeInTime;

            //Actually Teleporting
            //Vector3 dif = Teleportlocation.position - Player.position;
            //Player.position = Teleportlocation.position;
            //Player.rotation = Teleportlocation.rotation;

            bool hasDoneIt = false;
            foreach (Camera c in Camera.allCameras)
            {
                if ((c.name == "LeftEyeAnchor") && (!hasDoneIt))
                {
                    /*
                    GameObject sObj = new GameObject();
                    GameObject nObj = Instantiate(sObj);
                    c.transform.position = nObj.transform.position;
                    c.transform.parent = nObj.transform;
                    c.transform.localPosition = Vector3.zero;
                    nObj.transform.position = nObj.transform.position + (dif * 0.9f);
                    nObj.transform.rotation = Player.rotation;*/
                    Transform PlayPlat = GameObject.FindGameObjectWithTag("PlayerPlat").transform;
                    Transform PlatFace = GameObject.FindGameObjectWithTag("PlatformFacing").transform;
                    c.transform.parent.parent.parent.position = Teleportlocation.position;

                    PlayPlat.LookAt(PlatFace.position, Vector3.up);
                    Vector3 assignedEulers = PlayPlat.rotation.eulerAngles;
                    assignedEulers.x = 0;
                    assignedEulers.z = 0;
                    PlayPlat.rotation = Quaternion.Euler(assignedEulers);

                    hasDoneIt = true;
                }
            }


                //GameObject everything = GameObject.FindGameObjectWithTag("Finish");
                //everything.transform.position = everything.transform.position - Teleportlocation.localPosition;

                removeTeles();
            }

            if ((elapsedTime < 0) && (!fadeDirection))
            {

                elapsedTime = 0;
                TeleportRenderer.enabled = false;
                doTheFade = false;
                fadeDirection = true;
            }

            elapsedTime += (fadeDirection) ? Time.deltaTime : -Time.deltaTime;
            float divTime = ((fadeDirection) ? fadeOutTime : fadeInTime); ;
            float curAlpha = elapsedTime / divTime;

            Color curColor = TeleportRenderer.material.color;
            curColor.a = curAlpha;
            TeleportRenderer.material.color = curColor;

        }

    private void stopAllTheFade()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Teleport"))
        {
            try
            {
                if (g != this.gameObject) { g.GetComponent<Teleport>().setFade(false); }
            }
            catch { }
        }
    }

    public void setFade(bool f)
    {
        doTheFade = f;
    }

    private void removeTeles()
    {
        //Setup other teleporters
        foreach (GameObject Teleporter in GameObject.FindGameObjectsWithTag("Teleport"))
        { 
            if ((Teleporter.transform.parent) && (Teleporter.transform.parent.name != "ExtraContainer"))
            {
                if (Teleporter != this.gameObject)
                {
                    Teleporter.SetActive(false);
                }
                else
                {
                    foreach (Renderer r in Teleporter.GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = false; 
                    }
                }
            }
        }

        if (Visible.Count > 0)
        {
            foreach (GameObject Tele in Visible)
            {
                if (Tele) Tele.SetActive(true);
            }
        }
    }
}