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

            //bool hasDoneIt = false;
            /*foreach (Camera c in Camera.allCameras)
            {
                if ((c.name != "LeftEyeAnchor") && (c.name != "RightEyeAnchor") && (!hasDoneIt))
                {
                    c.transform.parent.position = Teleportlocation.position;
                    c.transform.parent.rotation = Teleportlocation.rotation;

                    hasDoneIt = true;
                }
            }*/

            GameObject c = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;
            c.transform.position = Teleportlocation.position;
            c.transform.rotation = Teleportlocation.rotation;

            GameObject plat = GameObject.FindGameObjectWithTag("PlayerPlatform");
            plat.transform.position = c.transform.position;
            plat.transform.rotation = Quaternion.Euler(new Vector3(plat.transform.rotation.eulerAngles.x,
                                                                   c.transform.rotation.eulerAngles.y,
                                                                   plat.transform.rotation.eulerAngles.z));

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
            if (Teleporter.gameObject != this.gameObject)
            {
                if (Teleporter.transform.parent.name != "SelectionContainer")
                {
                    foreach (Renderer ren in GetComponentsInChildren<Renderer>())
                    {
                        ren.enabled = true;
                    }

                    Teleporter.SetActive(false);
                }
            }
            else
            { 
                foreach (Renderer ren in GetComponentsInChildren<Renderer>())
                {
                    ren.enabled = false;
                }
            }
        }

        if (Visible.Count > 0)
        {
            foreach (GameObject Tele in Visible)
            {
                if (Tele)
                {
                    Tele.SetActive(true);
                    foreach (Renderer ren in GetComponentsInChildren<Renderer>())
                    {
                        ren.enabled = true;
                    }
                }
            }
        }
    }
}