using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraHandler : MonoBehaviour {

    public Text Extra1Price;
    public Text Extra1Desc;

    public Text Extra2Price;
    public Text Extra2Desc;

    public GameObject[] BaggerExtras;
    public GameObject[] LKWExtras;
    public GameObject[] KranExtras;

    public PricingScript PS;


    // Use this for initialization
    void Start () {
        
	}

    public void reset(string NO)
    {
        Extra1Price.text = "0€";
        Extra1Desc.text = "/";

        Extra2Price.text = "0€";
        Extra2Desc.text = "/";

        if(NO == "Bagger1")
        {
            foreach( GameObject X in BaggerExtras)
            {
                X.SetActive(true);
            }
            foreach (GameObject X in LKWExtras)
            {
                X.SetActive(false);
            }
            foreach (GameObject X in KranExtras)
            {
                X.SetActive(false);
            }
        }
    }

    public void apply(string CO, int Extra)
    {

            if (CO == "Bagger1" && Extra == 1)
            {
                Extra1Price.text = "Extra1:  700€";
                Extra1Desc.text = "König C23 Reifen";
            }
            else if(CO == "Bagger1" && Extra == 3)
            {
                Extra2Price.text = "Extra2: 1000€";
            Extra2Desc.text = "Cat 25 Schaufel";
            }
        
    }
}
