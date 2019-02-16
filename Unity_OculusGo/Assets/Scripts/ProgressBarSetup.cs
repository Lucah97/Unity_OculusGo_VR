using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBarSetup : MonoBehaviour {

    public static ProgressBarSetup instance;

    public bool enabled = true;

    private float curFill = 0f;
    private Image fillImg;

    private void Awake()
    {
        if (ProgressBarSetup.instance == null)
        {
            ProgressBarSetup.instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start () {
        fillImg = transform.GetChild(1).GetComponent<Image>();

        setEnabled(enabled);
        setCurrentProgress(0.4f);
    }

    public void setCurrentProgress(float p)
    {
        curFill = p;
        fillImg.fillAmount = curFill;
    }

    public void setEnabled(bool e)
    {
        enabled = e;

        transform.GetChild(0).GetComponent<Image>().enabled = enabled;
        transform.GetChild(1).GetComponent<Image>().enabled = enabled;
    }
}
