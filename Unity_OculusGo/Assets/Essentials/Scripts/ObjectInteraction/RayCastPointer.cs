using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPointer : MonoBehaviour {

    public bool mouseMode = false;

    [Header("Ray Cast Properties")]
    public Transform oculusPointer;
    public GameObject cursorPrefab;
    public float maxDistance;

    [Header("Line Properties")]
    public float maxLineDistance = 15f;
    public Vector2 alphaReach = new Vector2(0.3f, 0.85f);
    public float cursorScale = 1f;
    public float cardboardSelectionSpeed = 0.2f;

    private LineRenderer linRen;
    private GameObject cursor;
    private UI_Interaction lastHitObj;
    private float selectionProgress = 0f;

	void Start () {
        linRen = GetComponent<LineRenderer>();
        cursor = Instantiate(cursorPrefab, new Vector3(0, -9000, 0), Quaternion.identity);
        cursor.transform.localScale = new Vector3(cursorScale, cursorScale, cursorScale);
    }
	
	// Update is called once per frame
	void Update () {
        //Check Raycast from Controller
        RaycastHit hit;

        Vector3 normFWD = Camera.main.transform.forward;

        UI_Interaction curHitObj;
		if (Physics.Raycast((mouseMode) ? Camera.main.transform.position : oculusPointer.position, 
                            (mouseMode) ? normFWD : oculusPointer.forward, out hit, maxDistance))
        {
            cursor.transform.position = hit.point;
            if (hit.collider.CompareTag("Echo") || hit.collider.CompareTag("UI") || hit.collider.CompareTag("Teleport"))
            {
                //Check if same object for Cardboard
                if (!mouseMode)
                {
                    curHitObj = hit.collider.GetComponent<UI_Interaction>();
                    if (curHitObj == lastHitObj)
                    {
                        selectionProgress += (Time.deltaTime * cardboardSelectionSpeed);
                        if (selectionProgress >= 1)
                        {
                            cursor.GetComponent<CursorGraphics>().setCursorMode(true, true, false);
                            lastHitObj.interact();

                            ProgressBarSetup.instance.setEnabled(false);
                            selectionProgress = 0f;
                        }

                        ProgressBarSetup.instance.setEnabled(true);
                        ProgressBarSetup.instance.setCurrentProgress(selectionProgress);
                    }
                }

                //UI Ineraction Ray
                lastHitObj = hit.collider.GetComponent<UI_Interaction>();
                lastHitObj.rayEnter();

                //Set Cursor Graphics
                cursor.GetComponent<CursorGraphics>().setCursorMode(false, true, false);

                //VR Conroller Input
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
                {
                    cursor.GetComponent<CursorGraphics>().setCursorMode(true, true, false);
                    lastHitObj.interact();
                }
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonUp(0))
                {
                    cursor.GetComponent<CursorGraphics>().setCursorMode(false, true, false);
                }
            }
            else
            {
                checkLastObj();

                //Set Cursor Graphics
                cursor.GetComponent<CursorGraphics>().setCursorMode(true, false, false);

                //VR Conroller Input
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
                {
                    cursor.GetComponent<CursorGraphics>().setCursorMode(false, false, true);
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
                {
                    cursor.GetComponent<CursorGraphics>().setCursorMode(true, false, false);
                }
            }
        }
        else
        {
            checkLastObj();
            cursor.transform.position = new Vector3(0, -9000, 0);
        }

        //Setup Line Renderer
        Vector3[] vertices = new Vector3[2];
        vertices[0] = oculusPointer.position;
        vertices[1] = vertices[0] + (oculusPointer.forward * maxLineDistance);
        if (linRen.enabled)
        {
            linRen.SetVertexCount(2);
            linRen.SetPositions(vertices);
        }

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.black, 0.9f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, alphaReach.x), new GradientAlphaKey(0f, alphaReach.y) }
        );
        linRen.colorGradient = gradient;
    }

    private void checkLastObj()
    {
        try
        {
            if (lastHitObj.gameObject != null)
            {
                lastHitObj.rayExit();
                lastHitObj = null;
                ProgressBarSetup.instance.setEnabled(false);
                selectionProgress = 0f;
            }
        }
        catch { }
    }

    public void setCursorScale(float s)
    {
        cursorScale = s;
    }
}
