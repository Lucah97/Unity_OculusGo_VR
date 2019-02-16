using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlisseeActivate : MonoBehaviour, IObjInteractionTarget {

    public string animationName = "Opening";

	public void targetInteract(int v)
    {
        Animation anim = GetComponent<Animation>();
        float animLength = GetComponent<Animation>()[animationName].length;
        anim[animationName].speed = v;
        if (anim[animationName].time > animLength)
        {
            GetComponent<Animation>()[animationName].time = animLength;
        }
        else if(anim[animationName].time < 0f)
        {
            GetComponent<Animation>()[animationName].time = 0f;
        }
        anim.Play();
    }
}
