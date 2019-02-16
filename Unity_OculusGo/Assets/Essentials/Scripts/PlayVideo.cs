using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour, IObjInteractionTarget
{
    public VideoPlayer videoPlayer;

    public GameObject PauseButton;
    public GameObject Replaybutton;

    private float Videolength = 125f;
    private bool pause;
    public Vector3 OrigPos;

    public void Start()
    {
        PauseButton.SetActive(false);
        Replaybutton.SetActive(false);
        //Videolength = (float)videoPlayer.clip.length;
        OrigPos = transform.position;
    }

    public void targetInteract(int v)
    {
        videoPlayer.url = "file://" + Application.streamingAssetsPath + "/181227_Roadshow2019_ShopSystem_VR.webm";
        //Debug.Log(videoPlayer.url);
        //videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
        videoPlayer.Play();
        pause = false;

        PauseButton.SetActive(true);
        transform.position = new Vector3(0, -3000, 0);

        StartCoroutine(WaitForVideo());
    }

    public void Paused()
    {
        pause = true;
    }

    public void Setpause(bool p) { pause = p; }

    public void Videolenghtreset()
    {
        //try
        //{
            //Videolength = (float)videoPlayer.clip.length;
        //}
        //catch
        //{

        //}
        StartCoroutine(WaitForVideo());
    }

    IEnumerator WaitForVideo()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(Videolength);
        while (Videolength > 0 && !pause)
        {
            
            yield return waitForSeconds;
            break;
        }
        videoPlayer.Stop();
        Replaybutton.SetActive(true);
        transform.position = new Vector3(0, -3000, 0);
    }
}
