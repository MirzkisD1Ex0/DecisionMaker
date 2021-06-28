using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class OpeningController : MonoBehaviour
{
    private VideoPlayer vp;

    private void Awake()
    {
        vp = GameObject.Find("vp").GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        vp.loopPointReached += JumpToNextScene;
    }

    private void JumpToNextScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
        return;
    }
}