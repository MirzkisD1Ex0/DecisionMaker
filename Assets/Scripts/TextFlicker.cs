using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class TextFlicker : MonoBehaviour
{
    private float maxValue = 255;
    private float minValue = 102;
    private float lightSpeed = 20;
    private float darkSpeed = 15;


    private float floatingValue = 0;
    private bool isFull = false;
    private Color newColor = Color.white;
    private Text t;

    private void Awake()
    {
        t = GetComponent<Text>();
    }

    private void Update()
    {
        TextAlphaFlick();
    }

    private void TextAlphaFlick()
    {
        if (floatingValue < maxValue && !isFull)
        {
            floatingValue += Time.deltaTime * 10 * lightSpeed;
            if (floatingValue >= maxValue)
            {
                isFull = true;
            }
        }
        else if (floatingValue > minValue && isFull)
        {
            floatingValue -= Time.deltaTime * 10 * darkSpeed;
            if (floatingValue <= minValue)
            {
                isFull = false;
            }
        }
        newColor.a = floatingValue / 255;
        t.color = newColor;
    }
}