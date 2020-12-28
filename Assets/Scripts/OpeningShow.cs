using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class OpeningShow : MonoBehaviour
{
    [Header("Begin")]
    public RectTransform BeginRectCmpt;
    public Text BeginTextCmpt;
    private Tweener beginAnim;
    public float beginSpeed;

    [Header("Scale")]
    public RectTransform ImageRectCmpt;
    private Tweener scaleAnim;
    public float scaleSpeed;

    [Header("Title")]
    public Text TitleTextCmpt;
    private Tweener typerAnim;
    [SerializeField]
    private string title = "Decision Maker Plus";
    public float typerSpeed;

    [Header("Info")]
    public Text InfoTextCmpt;
    private Tweener infoAnim;
    public float infoSpeed;

    private void Start()
    {
        BeginRectCmpt.sizeDelta = new Vector2(-BeginRectCmpt.sizeDelta.x, BeginRectCmpt.sizeDelta.y);
        BeginTextCmpt.color = new Color(32f / 255f, 32f / 255, 32f / 255f, 0f);
        ImageRectCmpt.sizeDelta = new Vector2(ImageRectCmpt.sizeDelta.x, -Screen.height);
        TitleTextCmpt.text = string.Empty;
        InfoTextCmpt.text = string.Empty;

        BeginAnimation();
    }

    /// <summary>
    /// 
    /// </summary>
    private void BeginAnimation()
    {
        beginAnim.SetEase(Ease.Linear);
        beginAnim = BeginRectCmpt.DOSizeDelta(new Vector2(240f, BeginRectCmpt.sizeDelta.y), beginSpeed);
        beginAnim.OnComplete(() =>
        {
            BeginTextCmpt.DOFade(1f, beginSpeed);
        });
        return;
    }

    /// <summary>
    /// 按钮事件
    /// </summary>
    public void StartAnimation()
    {
        beginAnim = BeginRectCmpt.DOScale(new Vector3(0f, 0f, 0f), beginSpeed * .5f);
        beginAnim.OnComplete(() =>
        {
            ScaleAnimationPartOne();
        });
    }

    /// <summary>
    /// 
    /// </summary>
    private void ScaleAnimationPartOne()
    {
        scaleAnim.SetEase(Ease.Linear);
        scaleAnim = ImageRectCmpt.DOSizeDelta(new Vector2(ImageRectCmpt.sizeDelta.x, 130f), scaleSpeed);
        scaleAnim.OnComplete(() =>
        {
            TyperAnimation();
        });
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void TyperAnimation()
    {
        typerAnim.SetEase(Ease.Linear);
        typerAnim = TitleTextCmpt.DOText(title, typerSpeed);
        typerAnim.OnComplete(() =>
        {
            InfoAnimation();
        });
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void InfoAnimation()
    {
#if UNITY_STANDALONE_WIN
        string platformInfo = "Current Platform -Windows-";
#elif UNITY_WEBPLAYER || UNITY_EDITOR
        string platformInfo = "Current Platform -WebGL-";
#else
        string platformInfo = "Current Platform -Other-";
#endif
        infoAnim.SetEase(Ease.Linear);
        infoAnim = InfoTextCmpt.DOText(platformInfo, infoSpeed);
        infoAnim.OnComplete(() =>
        {
            ScaleAnimationPartTwo();
        });
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ScaleAnimationPartTwo()
    {
        scaleAnim = ImageRectCmpt.DOSizeDelta(new Vector2(Screen.width, ImageRectCmpt.sizeDelta.y), scaleSpeed);
        scaleAnim.OnComplete(() =>
        {
            ScaleAnimationPartThree();
        });
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ScaleAnimationPartThree()
    {
        scaleAnim = ImageRectCmpt.DOScaleY(0f, scaleSpeed);
        scaleAnim.OnComplete(() =>
        {
            StartCoroutine(NextScene());
        });
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}