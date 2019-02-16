using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Interaction : MonoBehaviour, IRayCastInteraction
{
    public bool active = true;
    public bool visible = true;

    private bool selected = false;
    private Renderer hlRenderer;

    // ### Unity Functions ###
    public virtual void Start()
    {
        //Get Highlight Renderer (2nd Child Object)
        hlRenderer = transform.GetChild(1).GetComponent<Renderer>();
        hlRenderer.enabled = false;
    }

    public virtual void FixedUpdate()
    {
        if (isActive())
        {
            hlRenderer.enabled = selected;
        }
        else
        {
            hlRenderer.enabled = false;
            setSelected(false);
        }
    }

    // ### RayCast Functions ###
    public virtual void rayEnter()
    {
        if (active && visible)
            selected = true;
    }

    public virtual void rayExit()
    {
        if (active && visible)
            selected = false;
    }

    public virtual void interact()
    {
        //Deriving Classes will implement here
    }

    // ### Getter / Setter ###
    public void setActive(bool nA)
    {
        active = nA;
    }
    public bool isActive()
    {
        return active;
    }

    public void setVisible(bool nV)
    {
        visible = nV;

        Renderer ren;
        foreach (Transform t in transform)
        {
            try
            {
                ren = t.GetComponent<Renderer>();
                ren.enabled = visible;
            }
            catch
            {}
        }
    }
    public bool isVisible()
    {
        return visible;
    }

    public void setSelected(bool sel)
    {
        selected = sel;
    }
    public bool isSelected()
    {
        return selected;
    }
}
