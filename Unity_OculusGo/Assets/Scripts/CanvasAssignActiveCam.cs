using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasAssignActiveCam : MonoBehaviour {

    public float timeUntilAssign = 0.6f;

	void Start () {
        StartCoroutine(setCam());
	}

    private IEnumerator setCam()
    {
        yield return new WaitForSeconds(timeUntilAssign);
        GetComponent<Canvas>().worldCamera = Camera.current;

        foreach (Camera c in Camera.allCameras)
        {
            c.transform.parent = transform.parent;
        }
    }
}
