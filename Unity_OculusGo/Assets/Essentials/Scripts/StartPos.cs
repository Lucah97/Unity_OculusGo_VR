using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour {

    public Transform Starten;

	// Use this for initialization
	void Start () {
        transform.parent.position = Starten.position;
        transform.parent.rotation = Starten.rotation;
	}
}
