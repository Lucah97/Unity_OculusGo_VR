using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Image_Button : UI_Button
{
    [Header("Image Properties")]
    public Texture2D canvasImage;
    public Texture2D[] icons = new Texture2D[2];
    public float scaleSpeed;

    private GameObject canvasContainer;
    private Vector3 canvasScale;
    private Vector3 desiredCanvasScale;

    private bool isEnlarged = false;

    public override void Start()
    {
        base.Start();
        canvasContainer = transform.GetChild(2).gameObject;
        canvasContainer.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", canvasImage);
        canvasScale = canvasContainer.transform.localScale;
        canvasContainer.transform.localScale = Vector3.zero;
        desiredCanvasScale = Vector3.zero;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        canvasContainer.transform.localScale = Vector3.Lerp(canvasContainer.transform.localScale,
                                                            desiredCanvasScale,
                                                            Time.deltaTime * scaleSpeed);
    }

    public override void interact()
    {
        if (isActive())
        {
            if (isEnlarged)
            {
                transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", icons[0]);
                desiredCanvasScale = Vector3.zero;
            }
            if (!isEnlarged)
            {
                transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", icons[1]);
                desiredCanvasScale = canvasScale;
            }

            isEnlarged = !isEnlarged;
        }
    }
}
