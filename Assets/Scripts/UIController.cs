using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 
/// </summary>
public class UIController : GlobalManager
{
    public GameObject OptionTemplate;

    private bool musicSwitch = true;
    private bool soundSwitch = true;
    private int quitCount = 0;

    private void Start()
    {
        AllUIDisappear();
        UIMove(MenuGO, false);
    }

    private void Update()
    {
        AppQuit();
    }

    #region UI弹出功能
    /// <summary>
    /// 功能弹出
    /// </summary>
    public void Button_FunctionPopup()
    {
        AllUIDisappear();
        UIMove(FunctionGO, false);
        return;
    }

    /// <summary>
    /// 功能弹出
    /// </summary>
    public void Button_SettingPopup()
    {
        AllUIDisappear();
        UIMove(SettingGO, false);
        return;
    }

    /// <summary>
    /// 菜单弹出
    /// </summary>
    public void Button_MenuPopup()
    {
        AllUIDisappear();
        UIMove(MenuGO, false);
        return;
    }
    #endregion

    #region UI功能
    /// <summary>
    /// 音乐开关按钮
    /// </summary>
    public void Button_MusicSwitch()
    {
        if (musicSwitch)
        {
            ahASCmpt.Stop();
        }
        else
        {
            ahASCmpt.Play();
        }
        musicSwitch = !musicSwitch;
        return;
    }

    /// <summary>
    /// 声音开关按钮
    /// </summary>
    public void Button_SoundSwitch()
    {
        if (soundSwitch)
        {
            CameraGO.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            CameraGO.GetComponent<AudioListener>().enabled = true;
        }
        soundSwitch = !soundSwitch;
        return;
    }

    /// <summary>
    /// 随机切换配色
    /// </summary>
    public void Button_SecretFunction()
    {
        Debug.Log("Nothing here.");
        return;
    }

    /// <summary>
    /// 输入选项数量限制
    /// </summary>
    public void InputField_OptionLimitCheck()
    {
        if (int.Parse(ocIFCmpt.text) < 0)
        {
            ocIFCmpt.text = "0";
        }
        if (int.Parse(ocIFCmpt.text) > 10)
        {
            ocIFCmpt.text = "10";
        }
        return;
    }

    /// <summary>
    /// 生成选项
    /// </summary>
    public void InputField_SpawnOptions()
    {
        for (int i = 0; i < OptionNodeGO.transform.childCount; i++) // 删除先前的
        {
            Destroy(OptionNodeGO.transform.GetChild(i).gameObject);
        }
        int spawnCount = int.Parse(ocIFCmpt.text);
        if (spawnCount > 0)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject tempGO = Instantiate(OptionTemplate, OptionNodeGO.transform);
                tempGO.name = "> Option " + string.Format("{0:D2}", (i + 1));
                tempGO.transform.GetChild(0).GetComponent<Text>().text = tempGO.name;
            }
        }
        return;
    }

    /// <summary>
    /// 决策
    /// </summary>
    public void Button_Decision()
    {
        int optionCount = OptionNodeGO.transform.childCount;
        if (optionCount <= 0)
        {
            return;
        }
        int theLuckyOne = Decition(optionCount);

        StartCoroutine(HighlightDisplay(OptionNodeGO, theLuckyOne));
        string resultText = "Your final choice is \"" + OptionNodeGO.transform.GetChild(theLuckyOne).GetComponent<InputField>().text + "\".";
        rTGO.text = "";
        rTGO.DOText(resultText, 3);
        return;
    }

    private IEnumerator HighlightDisplay(GameObject go, int number)
    {
        float speed = 2f;
        Image imageCmpt = go.transform.GetChild(number).GetComponent<Image>();
        imageCmpt.DOColor(new Color(217f / 255f, 185f / 255f, 242f / 255f), speed);
        yield return new WaitForSeconds(speed);
        imageCmpt.DOColor(Color.white, speed);
    }

    /// <summary>
    /// 决策核心
    /// </summary>
    /// <param name="optionCount"></param>
    /// <returns></returns>
    private int Decition(int optionCount)
    {
        float seed = Random.Range(0f, 100f);
        float rangePart = 100f / optionCount;

        for (int i = 0; i < optionCount; i++)
        {
            if (rangePart * i < seed && seed < rangePart * (i + 1))
            {
                return i;
            }
        }
        return 0;
    }
    #endregion

    #region UI移动功能
    /// <summary>
    /// 全部界面消失
    /// </summary>
    private void AllUIDisappear()
    {
        UIMove(MenuGO, true);
        UIMove(FunctionGO, true);
        UIMove(SettingGO, true);
        return;
    }

    /// <summary>
    /// DOTWEEN物体五向移动方法
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isPopup">是否要弹出/True出去/False回归</param>
    private void UIMove(GameObject go, bool isPopup)
    {
        Vector3 pos = Vector3.zero;
        if (isPopup)
        {
            int seed = Random.Range(0, 100) % 4;
            switch (seed)
            {
                default: pos = ScreenDirection.Left; break;
                case 0: pos = ScreenDirection.Left; break;
                case 1: pos = ScreenDirection.Down; break;
                case 3: pos = ScreenDirection.Right; break;
                case 4: pos = ScreenDirection.Up; break;
            }
        }
        else
        {
            pos = ScreenDirection.Center;
        }
        go.transform.DOLocalMove(pos, 1.0f).SetEase(Ease.InQuad);
        return;
    }

    /// <summary>
    /// 屏幕四方向
    /// </summary>
    class ScreenDirection
    {
        public static Vector3 Up = new Vector3(0, Screen.height, 0);
        public static Vector3 Down = new Vector3(0, -Screen.height, 0);
        public static Vector3 Right = new Vector3(Screen.width, 0, 0);
        public static Vector3 Left = new Vector3(-Screen.width, 0, 0);
        public static Vector3 Center = Vector3.zero;
    }
    #endregion

    #region Other
    private void AppQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGO.transform.DOLocalMove(ScreenDirection.Center, 1.0f).SetEase(Ease.InQuad);
            Invoke("QuitInfoDisplay", 3f);
            quitCount++;
            if (quitCount >= 2)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }

    private void QuitInfoDisplay()
    {
        QuitGO.transform.DOLocalMove(ScreenDirection.Down, 1.0f).SetEase(Ease.InQuad);
        quitCount = 0;
        CancelInvoke("QuitInfoDisplay");
    }
    #endregion
}