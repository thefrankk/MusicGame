
using System;
using DG.Tweening;
using UnityEngine;

public static class ExtendendMethods
{

    public static string EnumToString(this Enum value)
    {
        return Enum.GetName(value.GetType(), value);
    }

    public static Tween MoveTo(this Transform transform, Vector3 to, float duration, Action callback = null)
    {
        return transform.DOMove(to, duration).OnComplete(() => callback?.Invoke());
    }
    
    public static Tween ScaleTo(this Transform transform, Vector3 to, float duration, Action callback = null)
    {
        return transform.DOScale(to, duration).OnComplete(() => callback?.Invoke());
    }
    public static Tween ScaleToInOut(this Transform transform, Vector3 to, float duration, Action callback = null)
    {
        return transform.DOScale(to, duration).SetEase(Ease.InOutSine)
                                              .SetLoops(999, LoopType.Yoyo)
                                              .OnComplete(() => callback?.Invoke());
    }

    public static Tween ScaleToAndBack(this Transform transform, Vector3 to, float duration, Vector3 posOriginal, Action callback = null)
    {
        return transform.DOScale(to, duration)
                        .OnComplete(() =>
                        {
                            callback?.Invoke();
                            transform.DOScale(posOriginal, duration);
                        });
    }
}


