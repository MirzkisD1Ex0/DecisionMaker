using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
/// min max 左(正)下(正)右(负)上(负)
public class UIAdapter : MonoBehaviour
{
    // [Header("Percent")]
    public float LeftPercent, BottomPercent, RightPercent, TopPercent;

    private RectTransform goRT;

    private void Start()
    {
        goRT = GetComponent<RectTransform>();
        AutoSetRect(ref goRT, LeftPercent, BottomPercent, RightPercent, TopPercent);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="left"></param>
    /// <param name="bottom"></param>
    /// <param name="right"></param>
    /// <param name="top"></param>
    private void AutoSetRect(ref RectTransform rt, float left, float bottom, float right, float top)
    {
        rt.offsetMin = new Vector2(left * Screen.width, bottom * Screen.height);
        rt.offsetMax = new Vector2(-right * Screen.width, -top * Screen.height);
        return;
    }
}