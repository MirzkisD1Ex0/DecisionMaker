using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class OptionsSpawner : ObjectHub
{
    private InputField optionCountIFCmpt;

    private void Start()
    {
        optionCountIFCmpt = OptionCount.GetComponent<InputField>();
        optionCountIFCmpt.onEndEdit.AddListener(delegate
        {
            InstantiateDrive();
        });
    }

    private void InstantiateDrive()
    {
        if (string.IsNullOrEmpty(optionCountIFCmpt.text))
        {
            return;
        }

        int inputCount = int.Parse(optionCountIFCmpt.text);
        if (inputCount > OptionNode.transform.childCount)
        {
            AdjustmentOption(OptionTemplate, inputCount, OptionNode, Operation.Add);
        }
        else if (inputCount < OptionNode.transform.childCount)
        {
            AdjustmentOption(OptionTemplate, inputCount, OptionNode, Operation.Reduce);
        }
        else
        {
            return;
        }
        ResizeNodeSize(OptionNode, inputCount); // 根据输入数字来调节选项尺寸 // OptionNode在删除选项后子对象数量居然不变?代码执行需要时间的原因?
        return;
    }

    /// <summary>
    /// 调整选项
    /// </summary>
    /// <param name="objectGO">对象</param>
    /// <param name="objectCount">数量</param>
    /// <param name="objectNode">节点</param>
    /// <param name="objectOperation">操作</param>
    private void AdjustmentOption(GameObject objectGO, int objectCount, GameObject objectNode, Operation objectOperation)
    {
        switch (objectOperation)
        {
            default: break;
            case Operation.Add: // 增加
                int addCurrentCount = objectNode.transform.childCount;
                for (int i = addCurrentCount; i < objectCount; i++)
                {
                    GameObject instantGO = Instantiate(objectGO, objectNode.transform);
                    instantGO.name = i.ToString("00");
                    instantGO.transform.Find("Placeholder").gameObject.GetComponent<Text>().text = "OPTION " + (i + 1).ToString("00");
                }
                break;
            case Operation.Reduce: // 减少
                int reduceCurrentCount = objectNode.transform.childCount;
                for (int i = reduceCurrentCount - 1; i > objectCount - 1; i--)
                {
                    Destroy(objectNode.transform.GetChild(i).gameObject);
                }
                break;
        }
        return;
    }

    /// <summary>
    /// 调整节点尺寸
    /// </summary>
    /// <param name="objectNode"></param>
    /// <param name=""></param>
    private void ResizeNodeSize(GameObject objectNode, int objectCount)
    {
        RectTransform nodeTCpmt = objectNode.GetComponent<RectTransform>();
        RectTransform nodeChildTCpmt = objectNode.transform.GetChild(0).GetComponent<RectTransform>();
        float nodeXsize = nodeChildTCpmt.sizeDelta.x; // 子对象宽度
        float nodeYsize =
            nodeChildTCpmt.sizeDelta.y * objectCount + // 子对象累计高度
            objectNode.GetComponent<VerticalLayoutGroup>().spacing * (objectCount - 1); // 子对象累计间隙
        nodeTCpmt.sizeDelta = new Vector2(nodeXsize, nodeYsize);
        return;
    }

    enum Operation
    {
        Add,
        Reduce,
    }
}