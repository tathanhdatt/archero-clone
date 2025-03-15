using System.Collections;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;
using UnityEngine;

public class TimesPerSecondCondition : Condition
{
    [SerializeField, Required]
    private FloatVariable timesPerSecond;
    
    [SerializeField, ReadOnly]
    private float duration;

    private Coroutine delayCoroutine;

    protected override async UniTask OnEntered()
    {
        await base.OnEntered();
        this.isMet = false;
        this.duration = 1f / this.timesPerSecond.Value;
        if (this.delayCoroutine != null)
        {
            StopCoroutine(this.delayCoroutine);
        }

        this.delayCoroutine = StartCoroutine(Delay(duration));
    }

    private IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.isMet = true;
    }

    protected override async UniTask OnExited()
    {
        await base.OnExited();
        StopCoroutine(this.delayCoroutine);
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Wait {1f / this.timesPerSecond.Value}s";
    }
}