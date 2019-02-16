using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRolloAnimation : MonoBehaviour {

    public Animation[] associatedAnimations;
    public string animationName;

    private Animation curAnim;

	void Start ()
    {
        curAnim = GetComponent<Animation>();
	}

    void Update()
    {
        float animLength = curAnim[animationName].length;

        if ((curAnim[animationName].time < animLength) || (curAnim[animationName].time > 0))
        {
            syncAnimations();
        }
    }

    private void syncAnimations()
    {
        float curTime = curAnim[animationName].time;

        foreach (Animation an in associatedAnimations)
        {
            an.Play();
            an[animationName].time = curTime;
        }
    }
}
