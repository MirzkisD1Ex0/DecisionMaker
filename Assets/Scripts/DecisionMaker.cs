using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class DecisionMaker : ObjectHub
{
    private void Start()
    {
        DecisionButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            DecisionDrive();
        });
    }

    private void DecisionDrive()
    {
        if (OptionNode.transform.childCount == 0)
        {
            return;
        }
        DisplayResult(OptionNode, Decision(OptionNode.transform.childCount));
    }

    /// <summary>
    /// 决策
    /// </summary>
    /// <param name="objectCount"></param>
    /// <returns></returns>
    private int Decision(int objectCount)
    {
        float seed = Random.Range(0f, 100f);
        float rangePart = 100f / objectCount;

        for (int i = 0; i < objectCount; i++)
        {
            if (rangePart * i < seed && seed < rangePart * (i + 1))
            {
                
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// 显示结果
    /// </summary>
    /// <param name="objectNode"></param>
    /// <param name="resultIndex"></param>
    private void DisplayResult(GameObject objectNode, int resultIndex)
    {
        Debug.Log(objectNode.transform.GetChild(resultIndex).name);
    }
}