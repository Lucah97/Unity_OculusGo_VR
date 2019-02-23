using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionHandler : MonoBehaviour, IObjInteractionTarget
{
    [Header("Basic Properties")]
    public ColorChanger CC;
    public GameObject curObj;
    public GameObject Platform;

    private GameObject[] curExtras;

    public PricingScript PS;

    public Text Title;
    public Text Description;
    public Image Image;
    
    public Text BasePrice;
    public Text BaseDesc;

    [Header("Objects to be switched")]
    public CurrentObject[] curObjects = new CurrentObject[2];

    //JUCY LERP
    [Header("Jucy Lerp")]
    private bool fancyLerp = false;
    private Vector3 OrigPos;
    public float LerpBy = 2;
    public float LerpSpeed = 2.5f;

    private void Start()
    {
        curObj = curObjects[0].Object;
        curExtras = curObjects[0].Extras;
        Platform = curObjects[0].PlatformObj;
        givePrice(0);
    }

    void givePrice(int pos)
    {
        BasePrice.text = curObjects[pos]._Price;
        BaseDesc.text = curObjects[pos]._Name;

        PS.Base = curObjects[pos]._floatPrice;
    }

    public void targetInteract(int v)
    {
        if (!fancyLerp)
        {
            switchToObj(v);
        }
    }

    private void switchToObj(int O)
    {
        if (curObjects[O].Object != curObj)
        {
            apply(curObjects[O]);
            givePrice(O);
        }
    }

    void apply(CurrentObject CO)
    {
        Title.text = CO._Name;
        Description.text = CO._Description;
        Image.sprite = CO._Img;

        if (curObj != null)
        {
            curObj.SetActive(false);
            Platform.SetActive(false);

            //Set the Extras
            foreach (GameObject Ex in curExtras)
            {
                Ex.SetActive(false);
            }
        }

        curObj = CO.Object;
        Platform = CO.PlatformObj;
        curExtras = CO.Extras;

        jucyLerp();

        curObj.SetActive(true);
        Platform.SetActive(true);

        //RELOCATE PLAYER
        //GameObject.FindGameObjectWithTag("Player").transform.position = CO.Playerpos.position;
        //GameObject.FindGameObjectWithTag("Player").transform.rotation = CO.Playerpos.rotation;

        //Set the Extras
        foreach(GameObject Ex in curExtras)
        {
            Ex.SetActive(true);
        }

        CC.SetObjColor();
    }

    private void jucyLerp()
    {
        OrigPos = curObj.transform.position;
        Vector3 newPos = new Vector3(OrigPos.x, OrigPos.y + LerpBy, OrigPos.z);
        curObj.transform.position = newPos;
        fancyLerp = true;
    }

    private void Update()
    {
        if (fancyLerp)
        {
            if (Vector3.Distance(curObj.transform.position, OrigPos) > 0.02)
            {
                if (LerpSpeed > 0.25)
                {
                    LerpSpeed -= 0.001f;
                }
                curObj.transform.position = new Vector3(OrigPos.x, curObj.transform.position.y - Time.deltaTime * LerpSpeed, OrigPos.z);

                //SafetyFix
                if(curObj.transform.position.y <= OrigPos.y)
                {
                    curObj.transform.position = OrigPos;
                    fancyLerp = false;
                    LerpSpeed = 3;
                }
            }
            else
            {
                curObj.transform.position = OrigPos;
                fancyLerp = false;
                LerpSpeed = 3;
            }
        }
    }
}
[System.Serializable]
public class CurrentObject
{
    public string _Name;
    [TextArea]
    public string _Description;
    public string _Price;
    public Sprite _Img;
    public float _floatPrice;
    public GameObject Object;
    public GameObject PlatformObj;

   // public Transform Playerpos;

    public GameObject[] Extras = new GameObject[2];
}
