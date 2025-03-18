using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dt.Attribute;
using Dt.Condition.Core;
using MEC;
using UnityEngine;

public class TimesPerSecondCondition : Condition
{
    [SerializeField, Required]
    private FloatVariable timesPerSecond;
    
    [SerializeField, ReadOnly]
    private float duration;

    private CoroutineHandle delayCoroutine;

    protected override async UniTask OnEntered()
    {
        await base.OnEntered();
        this.isMet = false;
        this.duration = 1f / this.timesPerSecond.Value;
        if (this.delayCoroutine != default)
        {
            Timing.KillCoroutines(this.delayCoroutine);
        }

        this.delayCoroutine = Timing.RunCoroutine(Delay(this.duration));
    }

    private IEnumerator<float> Delay(float seconds)
    {
        yield return Timing.WaitForSeconds(seconds);
        this.isMet = true;
    }

    protected override async UniTask OnExited()
    {
        await base.OnExited();
        Timing.KillCoroutines(this.delayCoroutine);
    }

    [Button]
    private void Rename()
    {
        gameObject.name = $"Wait {1f / this.timesPerSecond.Value}s";
    }
}