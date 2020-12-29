using UnityEngine;

public class ProjectSetting : MonoBehaviour
{
    private int frameRate = 30;

    private void Awake()
    {
        Application.targetFrameRate = frameRate;
    }
}