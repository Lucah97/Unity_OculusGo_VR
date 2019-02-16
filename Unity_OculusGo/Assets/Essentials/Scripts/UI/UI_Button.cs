using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : UI_Interaction {

    public Transform[] targets;
    public int[] passVariables;
    public GameObject addedEcho = null;

    [Header("Animation Properties")]
    public bool addAnimations = false;
    public bool swapActiveButtons = false;
    public float animationSpeed = 1f;
    public float scaleMult = 1.3f;
    public float deactiveOpacity = 0.3f;

    private Vector3 normalScale;

	public override void Start () {
        base.Start();
        normalScale = transform.localScale;
	}
	
	public override void FixedUpdate () {
        base.FixedUpdate();

        if (addAnimations)
        {
            //Adjust Scale
            Vector3 desiredScale = (isSelected() && isActive()) ? (normalScale * scaleMult) : (normalScale);
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * animationSpeed);

            //Adjust Opacity
            Renderer ren = transform.GetChild(0).GetComponent<Renderer>();
            Color nCol = ren.material.color;
            nCol.r = (isActive()) ? 1f : deactiveOpacity;
            nCol.g = (isActive()) ? 1f : deactiveOpacity;
            nCol.b = (isActive()) ? 1f : deactiveOpacity;
            ren.material.color = nCol;
        }
    }

    public override void interact()
    {
        if (isActive())
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<IObjInteractionTarget>().targetInteract(passVariables[i]);
            }

            if (swapActiveButtons)
            {
                foreach (Transform t in transform.parent)
                {
                    if (t != transform)
                    {
                        t.GetComponent<UI_Button>().setActive(true);
                        t.GetComponent<Collider>().enabled = true;
                        this.GetComponent<Collider>().enabled = false;
                        this.setActive(false);
                    }
                }
            }

            if (addedEcho != null)
            {
                addedEcho.GetComponent<EchoInteraction>().flipWhichButton();
            }
        }
    }
}
