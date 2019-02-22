using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionHandler : MonoBehaviour, IObjInteractionTarget
{
    public ColorChanger CC;
    public GameObject curObj;
    public GameObject Platform;

    public PricingScript PS;

    public Text Title;
    public Text Description;
    public Image Image;
    
    public Text BasePrice;
    public Text BaseDesc;

    public CurrentObject[] curObjects = new CurrentObject[2];

    private void Start()
    {
        curObj = curObjects[0].Object;
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
        switchToObj(v);
    }

    private void switchToObj(int O)
    {
        apply(curObjects[O]);
        givePrice(O);
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
        }

        curObj = CO.Object;
        Platform = CO.PlatformObj;

        curObj.SetActive(true);
        Platform.SetActive(true);

        GameObject.FindGameObjectWithTag("Player").transform.position = CO.Playerpos.position;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = CO.Playerpos.rotation;

        CC.SetObjColor();
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

    public Transform Playerpos;
}
