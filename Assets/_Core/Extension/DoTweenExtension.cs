using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public static class DoTweenExtension
{
    public static UniTask ToUniTask1(this Tween tween, CancellationToken cancellationToken = default)
    {
        if (tween == null) return UniTask.CompletedTask;
        
        UniTaskCompletionSource source = new UniTaskCompletionSource();

        if (tween.IsComplete())
        {
            source.TrySetResult();
            return source.Task;
        }
        
        tween.OnComplete(OnComplete);
        
        if (cancellationToken.CanBeCanceled)
        {
            cancellationToken.Register(() =>
            {
                if (!tween.IsActive() || tween.IsComplete()) return;
                tween.Kill();
                source.TrySetCanceled();
            });
        }
        
        return source.Task;

        void OnComplete()
        {
            source.TrySetResult();
        }
    }
}