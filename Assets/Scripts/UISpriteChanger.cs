using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 强制按钮_点击后切换图片
/// </summary>
public class UISpriteChanger : MonoBehaviour
{
    public Sprite On;
    public Sprite Off;

    private Image imageCmpt;
    private Button buttonCmpt;
    private bool buttonSwitch = true;

    private void Start()
    {
        imageCmpt = GetComponent<Image>();
        buttonCmpt = GetComponent<Button>();
        buttonCmpt.onClick.AddListener(ChangeSprite);
    }

    private void ChangeSprite()
    {
        if (buttonSwitch)
        {
            imageCmpt.sprite = Off;
        }
        else
        {
            imageCmpt.sprite = On;
        }
        buttonSwitch = !buttonSwitch;
    }
}