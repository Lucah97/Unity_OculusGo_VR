using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ReplayVideo : MonoBehaviour, IObjInteractionTarget
{
    public VideoPlayer videoPlayer;

    public GameObject Playbutton;
    public GameObject PauseButton;

    public void targetInteract(int v)
    {
        videoPlayer.Play();
        Playbutton.GetComponent<PlayVideo>().Setpause(false);

        PauseButton.SetActive(true);

        Playbutton.GetComponent<PlayVideo>().Videolenghtreset();

        gameObject.SetActive(false);

    }
}
