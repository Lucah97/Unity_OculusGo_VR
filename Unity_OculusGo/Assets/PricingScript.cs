using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PricingScript : MonoBehaviour {

    public float Extra1;
    public float Extra2;
    public float Base;
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "Gesamt: " + (Extra1 + Extra2 + Base) + "€";
	}
}
