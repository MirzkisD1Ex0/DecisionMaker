using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    protected static bool scriptLimit = false;

    protected static GameObject MenuGO;
    protected static GameObject FunctionGO;
    protected static GameObject SettingGO;
    protected static GameObject QuitGO;

    protected static GameObject CameraGO;
    protected static GameObject AudioHubGO;
    protected static AudioSource ahASCmpt;
    protected static GameObject OptionCountGO;
    protected static InputField ocIFCmpt;
    protected static GameObject OptionNodeGO;
    protected static GameObject ResultGO;
    protected static Text rTGO;

    private void Awake()
    {
        if (scriptLimit)
        {
            return;
        }
        MenuGO = GOSeacher("menu");
        FunctionGO = GOSeacher("function");
        SettingGO = GOSeacher("setting");
        QuitGO = GOSeacher("quit");

        CameraGO = Camera.main.gameObject;

        AudioHubGO = GOSeacher("AudioHub");
        ahASCmpt = AudioHubGO.GetComponent<AudioSource>();

        OptionCountGO = GOSeacher("OptionCount");
        ocIFCmpt = OptionCountGO.GetComponent<InputField>();

        OptionNodeGO = GOSeacher("OptionNode");

        ResultGO = GOSeacher("Result");
        rTGO = ResultGO.GetComponent<Text>();

        scriptLimit = true;
    }

    /// <summary>
    /// 对象Get器
    /// </summary>
    /// <param name="goName"></param>
    /// <returns></returns>
    private GameObject GOSeacher(string goName)
    {
        GameObject tempGO = GameObject.Find(goName);
        if (tempGO)
        {
            return tempGO;
        }
        else
        {
            Debug.LogError("Cant found GO: " + goName);
            return null;
        }
    }
}