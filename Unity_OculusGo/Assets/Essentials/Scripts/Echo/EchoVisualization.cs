using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVisualization : MonoBehaviour {

    [Header("Ring variables")]
    public float blinkTime = 4f;
    public float blinkSpeed = 1f;
    public float blendTime = 1f;

    private Material blendMat;
    private float elapsedTime = 0;
    private bool lightIsActive = false;
    private float curRingOffset = 0f;

    private Renderer ren;
    private Color blendColor = Color.black;

    // Use this for initialization
    void Start () {
        ren = GetComponent<Renderer>();
        ren.materials[3].SetColor("_EmissionColor", blendColor);
	}
	
	// Update is called once per frame
	void Update () {
		//Enable light and animation when lightIsActive
        if (lightIsActive)
        {
            //Blend Emission Color from black to white
            blendColor = Color.Lerp(blendColor, 
                                    (elapsedTime < (blinkTime / 2)) ? Color.white : Color.black, 
                                    Time.deltaTime * blendTime);
            ren.materials[3].SetColor("_EmissionColor", blendColor);

            //
            ren.materials[3].SetTextureOffset("_MainTex", new Vector2(curRingOffset, 0));
            curRingOffset += (blinkSpeed * Time.deltaTime);
            
            //Adding / Checking Time
            elapsedTime += Time.deltaTime;
            if (elapsedTime > blinkTime)
            {
                elapsedTime = 0f;
                curRingOffset = 0f;
                lightIsActive = false;
            }
        }
	}

    public void enableRingLight()
    {
        lightIsActive = true;
        elapsedTime = 0f;
    }
}
