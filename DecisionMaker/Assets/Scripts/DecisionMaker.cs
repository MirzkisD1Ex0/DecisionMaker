using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

namespace DicisionMaker
{
  public class DecisionMaker : MonoBehaviour
  {
    [Header("自定义参数")]
    public int MaxOptionCount = 20;
    public Color HighlightColor;
    public Color LowlightColor;

    [Header("选项生成")]
    public InputField OptionCountIF; // 选项数量输入
    public Transform NodeOptions; // 选项父节点
    public GameObject OptionTemplate; // 选项模板

    public List<GameObject> OptionList = new List<GameObject>();

    public UIButton DecideButton;

    private void Start()
    {
      Setup();
    }



    private void Setup()
    {
      OptionCountIF.onEndEdit.AddListener((count) => { SpawnOptionCells(int.Parse(count)); });
      return;
    }

    /// <summary>
    /// 生成对应数量的选项
    /// 销毁多余的选项
    /// </summary>
    /// <param name="count"></param>
    private void SpawnOptionCells(int count)
    {
      if (count < 0)
      {
        count = 0;
        return;
      }

      int index = count - OptionList.Count;
      if (index > 0)
      {
        for (int i = 0; i < index; i++)
        {
          OptionList.Add(SpawnOptions());
        }
      }
      else if (index < 0)
      {
        index = Mathf.Abs(index);
        for (int i = 0; i < index; i++)
        {
          GameObject tempGO = OptionList[OptionList.Count - 1];
          Destroy(tempGO);
          OptionList.Remove(tempGO);
        }
      }
      return;
    }

    private GameObject SpawnOptions()
    {
      GameObject tempGO = Instantiate(OptionTemplate, NodeOptions);
      tempGO.name = "InputField - Option -" + tempGO.transform.GetSiblingIndex().ToString("00");
      tempGO.GetComponent<Image>().color = LowlightColor;
      tempGO.transform.Find("Image - Filler").GetComponent<Image>().color = new Color(0, 0, 0, .25f);
      return tempGO;
    }

    /// <summary>
    /// 选项数量矫正
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private int SpawnCountAdjust(int count)
    {
      if (count < 0)
      {
        count = 0;
      }
      if (count > MaxOptionCount)
      {
        count = MaxOptionCount;
      }
      return count;
    }

    // ____________________

    /// <summary>
    /// 
    /// </summary>
    public void MakeDecision()
    {
      if (OptionList.Count <= 1)
      {
        return;
      }
      if (resultGO != null)
      {
        LowlightResult();
      }

      int resultIndex = Random.Range(0, OptionList.Count);
      resultGO = OptionList[resultIndex];
      HighlightResult();
      PopupResult(resultGO.GetComponent<InputField>().text);

      return;
    }

    private GameObject resultGO;
    private void HighlightResult()
    {
      resultGO.GetComponent<Image>().color = HighlightColor;
      resultGO.transform.Find("Image - Filler").GetComponent<Image>().color = HighlightColor;
      return;
    }
    private void LowlightResult()
    {
      resultGO.GetComponent<Image>().color = LowlightColor;
      resultGO.transform.Find("Image - Filler").GetComponent<Image>().color = new Color(0, 0, 0, .25f);
      return;
    }

    private void PopupResult(string result)
    {
      UIPopupManager.ShowPopup("Decide", false, false);
      Text tempText = GameObject.Find("UIPopup - Decide(Clone)").transform.Find("Container/Message").GetComponent<Text>();
      tempText.text = result;
      return;
    }
  }
}