using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyLight : MonoBehaviour {

    public Transform BottomPiece;
    public float Multiplyer;

    private Material Light;
    private float Plisseeheight = 0;
    private float startPliseheight;

	// Use this for initialization
	void Start () {
        Light = GetComponent<Renderer>().material;
        startPliseheight = BottomPiece.transform.position.y;

        Transform currentBone = BottomPiece;
        while (currentBone.childCount > 0)
        {
            currentBone = currentBone.GetChild(0);
        }
        BottomPiece = currentBone;
	}
	
	// Update is called once per frame
	void Update () {
        Plisseeheight = BottomPiece.position.y;
        Vector2 Shift = new Vector2(0, (1 - Mathf.Abs(Plisseeheight * Multiplyer)));
        Light.SetTextureOffset("_Mask", Shift);

	}
}
