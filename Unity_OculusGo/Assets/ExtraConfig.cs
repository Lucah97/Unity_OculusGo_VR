using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraConfig : MonoBehaviour, IObjInteractionTarget {

    public ExtraItem[] extras = new ExtraItem[3];

    public TextMesh title;
    public TextMesh description;
    public TextMesh price;
    public Text PreisListe;
    public Text ListenBeschreibung;
    public SpriteRenderer image;

    public PricingScript PS;

    public GameObject curObj;
    public GameObject curPlat;

    public int Extra;

    private int curExtra = 0;


	void Start () {
        applyExtra();
        givePrice();
        curObj = extras[curExtra]._model;
        curPlat = extras[curExtra]._model;
    }

    private void applyExtra()
    {
        title.text = extras[curExtra]._title;
        description.text = extras[curExtra]._description;
        //description.text = description.text.Replace("\n", "\n");
        price.text = extras[curExtra]._price;
        PreisListe.text = extras[curExtra]._price;
        ListenBeschreibung.text = extras[curExtra]._title;
        image.sprite = extras[curExtra]._image;

        if (curObj != null)
        {
            curObj.SetActive(false);
            curPlat.SetActive(false);
        }

        curObj = extras[curExtra]._model;
        curPlat = extras[curExtra]._model;

        curObj.SetActive(true);
        curPlat.SetActive(true);

        givePrice();
    }

    void givePrice()
    {
        if (Extra == 1)
        {
            PS.Extra1 = extras[curExtra]._floatPrice;
        }
        else if(Extra == 2)
        {
            PS.Extra2 = extras[curExtra]._floatPrice;
        }
    }

    public void targetInteract(int v)
    {
        incExtra(v);
    }

    public void incExtra(int add)
    {
        curExtra += add;

        if (curExtra > (extras.Length - 1))
        {
            curExtra -= (extras.Length);
        }
        if (curExtra < 0)
        {
            curExtra += (extras.Length);
        }

        Debug.Log(curExtra);

        applyExtra();
    }
}

[System.Serializable]
public class ExtraItem
{
    public string _title;
    [TextArea]
    public string _description;
    public string _price;
    public float _floatPrice;
    public Sprite _image;
    public GameObject _model;
    public GameObject _Platmodel;
}