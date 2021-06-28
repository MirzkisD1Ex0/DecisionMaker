using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ObjectHub : MonoBehaviour
{
    #region ObjectName
    private string optionNodeName = "optionNode";
    private string optionTemplateName = "optionTemplate";
    private string optionCountName = "optionCount";
    #endregion

    #region Object
    protected static GameObject OptionNode;
    protected static GameObject OptionTemplate;
    protected static GameObject OptionCount;
    protected static GameObject DecisionButton;
    #endregion

    private void Awake()
    {
        FindNecessaryObjects();
    }

    private void FindNecessaryObjects()
    {
        OptionNode = GameObject.Find(optionNodeName).gameObject;
        OptionTemplate = GameObject.Find(optionTemplateName).gameObject;
        OptionCount = GameObject.Find(optionCountName).gameObject;
        DecisionButton = GameObject.Find("decision").gameObject;
    }
}