using DG.Tweening;
using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    [Header("References")]
    [SerializeField, Required]
    private Image image;

    [Header("Configs")]
    [SerializeField]
    private float fadeInValue;

    [SerializeField]
    private float fadeInDuration;

    [SerializeField]
    private Ease fadeInEase;

    [SerializeField]
    private float fadeOutValue;

    [SerializeField]
    private float fadeOutDuration;

    [SerializeField]
    private Ease fadeOutEase;

    private Tween tween;

    public void Fade()
    {
        this.tween?.Kill();
        this.tween = this.image.DOFade(this.fadeInValue, this.fadeInDuration);
        this.tween.SetEase(this.fadeInEase);
        this.tween.OnComplete(OnFadeInCompleteHandler);
    }

    private void OnFadeInCompleteHandler()
    {
        this.tween = this.image.DOFade(this.fadeOutValue, this.fadeInDuration);
        this.tween.SetEase(this.fadeOutEase);
    }
}