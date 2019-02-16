using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleDetail : MonoBehaviour {

    public string text = "Text";
    [Range(5,75)]
    public int textSize = 14;
    public bool bubbleType = true;
    public Material[] bubbleMaterials = new Material[2];

    public void setupBubble(string nText, int nTextSize, bool nBubbleType)
    {
        text = nText;
        textSize = nTextSize;
        bubbleType = nBubbleType;

        createBubble();
    }

    public void createBubble()
    {
        Vector3 anchorLeft = transform.GetChild(1).localPosition;
        Vector3 anchorRight = transform.GetChild(2).localPosition;

        TextMesh actText;
        actText = transform.GetChild(0).GetComponent<TextMesh>();

        actText.text = text;
        actText.fontSize = textSize;
        actText.color = (bubbleType) ? Color.white : Color.black;
        actText.anchor = (bubbleType) ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
        actText.transform.localPosition = (bubbleType) ? anchorRight : anchorLeft;

        GetComponent<Renderer>().material = (bubbleType) ? bubbleMaterials[0] : bubbleMaterials[1];
    }


	
}
