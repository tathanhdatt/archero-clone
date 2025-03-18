using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dt.Attribute;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField, Required]
    private TMP_Text text;

    [SerializeField]
    private Vector3 targetSize;

    [SerializeField]
    private float duration;
    
    [SerializeField]
    private Ease ease;
    
    private Tweener tweener;

    public void Initialize(string content)
    {
        Initialize(content, this.text.fontSize);
    }
    
    public async void Initialize(string content, float fontsize)
    {
        ResetScale();
        this.text.SetText(content);
        this.text.fontSize = fontsize;
        await ScaleUp();
        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        this.text.color = color;
    }

    private void ResetScale()
    {
        transform.localScale = Vector3.zero;
    }

    private async UniTask ScaleUp()
    {
        this.tweener?.Kill();
        this.tweener = transform.DOScale(this.targetSize, this.duration);
        this.tweener.SetEase(this.ease);
        await this.tweener.AsyncWaitForCompletion();
    }
}