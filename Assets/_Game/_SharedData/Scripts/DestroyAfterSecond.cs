using DG.Tweening;
using UnityEngine;

public class DestroyAfterSecond : MonoBehaviour
{
    [SerializeField]
    private float seconds;
    
    private float elapsedTime;
    
    private bool destroyed;

    private void OnEnable()
    {
        this.elapsedTime = 0f;
        this.destroyed = false;
        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (this.destroyed) return;
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime < this.seconds) return;
        ScaleDown();
        this.destroyed = true;
    }

    private void ScaleDown()
    {
        transform.DOScale(Vector3.zero, 0.2f)
            .OnComplete(()=>Destroy(gameObject));
    }
}