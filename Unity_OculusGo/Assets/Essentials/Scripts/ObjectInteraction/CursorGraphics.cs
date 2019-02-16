using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGraphics : MonoBehaviour {

    public bool showNormalCursor = true;
    public bool showSelectCursor = false;
    public bool showRedCursor = false;

    public void setCursorMode(bool norm, bool sel, bool red)
    {
        showNormalCursor = norm;
        showSelectCursor = sel;
        showRedCursor = red;

        assignCursorMode();
    }

    private void assignCursorMode()
    {
        transform.GetChild(0).GetComponent<Renderer>().enabled = showNormalCursor;
        transform.GetChild(1).GetComponent<Renderer>().enabled = showSelectCursor;
        transform.GetChild(2).GetComponent<Renderer>().enabled = showRedCursor;
    }

    void Start()
    {
        assignCursorMode();
    }
}
