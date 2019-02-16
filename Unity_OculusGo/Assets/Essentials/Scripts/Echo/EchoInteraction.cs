using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoInteraction : MonoBehaviour, IObjInteractionTarget {

    [Header("Assigned Prefabs")]
    public GameObject Prefab_SpeechBubble;
    public GameObject AlexaPlisseeKontrolle;

    [Header("Misc. Properties")]
    public string associatedTag = "PlSet1";
    public int maxChatAmount = 5;
    public float chatGap = 0.5f;

    [Header("Speachbubbles")]
    public string Speachbubble1;
    public string Speachbubble2;

    private List<GameObject> echoChat = new List<GameObject>();
    private bool hasBeenActivated = false;
    private bool whichButton = true;


    public GameObject spawnSpeechBubble(string text, int textSize, bool type)
    {
        GameObject nBub = Instantiate(Prefab_SpeechBubble,
                                      transform.position,
                                      Prefab_SpeechBubble.transform.rotation);

        nBub.GetComponent<SpeechBubbleDetail>().setupBubble(text, textSize, type);

        echoChat.Add(nBub);
        addChatHeight(chatGap);
        checkRemoveChat();

        enableEchoLight();

        return nBub;
    }

    public GameObject spawnCustom(GameObject spawnObj, float scaleMult, float addY)
    {
        GameObject nSpawn = Instantiate(spawnObj,
                                        transform.position,
                                        spawnObj.transform.rotation);

        Vector3 nScale = nSpawn.transform.localScale;
        nScale *= scaleMult;
        nSpawn.transform.localScale = nScale;

        echoChat.Add(nSpawn);
        addChatHeight(addY);
        checkRemoveChat();

        return nSpawn;
    }

    private void addChatHeight(float addY)
    {
        foreach(GameObject g in echoChat)
        {
            Vector3 curPos = g.transform.localPosition;
            curPos.y += Mathf.Abs(addY);
            g.transform.localPosition = curPos;
        }
    }

    private void checkRemoveChat()
    {
        if (echoChat.Count > maxChatAmount)
        {
            Destroy(echoChat.ToArray()[0]);
            echoChat.RemoveAt(0);
        }
    }

    private void enableEchoLight()
    {
        GetComponent<EchoVisualization>().enableRingLight();
    }

    // ### Target ###
    public void targetInteract(int v)
    {
        if (!hasBeenActivated)
        {
            StartCoroutine(AlexaTalk());
            transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
            hasBeenActivated = true;
        }
        else
        {
            deleteChat();
            hasBeenActivated = false;

            transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
        }
        
    }

    IEnumerator AlexaTalk()
    {
        spawnSpeechBubble(Speachbubble1, 37, true);
        yield return new WaitForSeconds(3.1f);
        spawnSpeechBubble(Speachbubble2, 37, false);
        yield return new WaitForSeconds(2.2f);
        addChatHeight(0.1f);
        GameObject ctrls = spawnCustom(AlexaPlisseeKontrolle, 1f, 0.25f);

        //Assign Plissee to controller
        GameObject[] PlisseeSet = GameObject.FindGameObjectsWithTag(associatedTag);
        for (int i=0; i<PlisseeSet.Length; i++)
        {
            ctrls.transform.GetChild(0).GetComponent<UI_Button>().targets[i] = PlisseeSet[i].transform;
            ctrls.transform.GetChild(1).GetComponent<UI_Button>().targets[i] = PlisseeSet[i].transform;
        }

        //Disable Last unused button
        int whichChild = (whichButton) ? 1 : 0;
        ctrls.transform.GetChild(whichChild).GetComponent<UI_Button>().setActive(false);
    }

    public void deleteChat()
    {
        StopAllCoroutines();
        foreach (GameObject g in echoChat)
        {
            GameObject.Destroy(g);
        }
        echoChat = new List<GameObject>();

        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
        hasBeenActivated = false;
    }

    public void flipWhichButton()
    {
        whichButton = !whichButton;
    }
}
