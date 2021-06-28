using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

/// <summary>
/// 
/// </summary>
public class NewBehaviourScript : MonoBehaviour
{
    public Text TextCmpt;
    private Tweener textAnim;

    private void Start()
    {
        textAnim.SetEase(Ease.Linear);
        TypeAnimation();
    }

    private void TypeAnimation()
    {
        TextCmpt.text = string.Empty;
        textAnim = TextCmpt.DOText("<MirzkisD1Ex0@admin> : Nothing here right now.\n<MirzkisD1Ex0@admin> : But don't worry.\n<MirzkisD1Ex0@admin> : There will be something soon...\n<MirzkisD1Ex0@admin> : Trust me. -_-!!!", 12f);
        textAnim.OnComplete(() =>
        {
            TextAnimationPartOne();
        });
        return;
    }

    private void TextAnimationPartOne()
    {
        textAnim = TextCmpt.DOColor(Color.gray, 2f);
        textAnim.OnComplete(() =>
        {
            TextAnimationPartTwo();
        });
        return;
    }

    private void TextAnimationPartTwo()
    {
        textAnim = TextCmpt.DOColor(Color.white, 2f);
        textAnim.OnComplete(() =>
        {
            TextAnimationPartOne();
        });
        return;
    }
}