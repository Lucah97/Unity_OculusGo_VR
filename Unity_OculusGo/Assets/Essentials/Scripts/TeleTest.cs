using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleTest : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Tel_living3_wohn").GetComponent<Teleport>().targetInteract(0); 
        }
	}
}
