using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseVideo : MonoBehaviour, IObjInteractionTarget
{
    public VideoPlayer videoPlayer;
    public GameObject Playbutton;

    public void targetInteract(int v)
    {
        videoPlayer.Pause();
        Playbutton.GetComponent<PlayVideo>().Paused();

        Playbutton.transform.position = Playbutton.GetComponent<PlayVideo>().OrigPos;
        gameObject.SetActive(false);
    }
}
