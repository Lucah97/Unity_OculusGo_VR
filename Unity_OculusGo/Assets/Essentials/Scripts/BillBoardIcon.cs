using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardIcon : MonoBehaviour {

    public Vector3 addRot = new Vector3(90, 0, 0);

    void LateUpdate () {
        Vector3 lookPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        transform.LookAt(lookPos);
        transform.Rotate(addRot);
	}
}
