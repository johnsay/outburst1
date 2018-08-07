#if DOTWEEN
using DG.Tweening;
using UnityEngine;
using System;

public class UiTween 
{
    public delegate void UiTweenCallBack();
    
    public static Sequence BuildUiSequence(Tween[] tweens, UiTweenCallBack callback = null)
    {
        Sequence tweenSequence = DOTween.Sequence();
        foreach (var t in tweens)
        {
            tweenSequence.Join(t);
        }

        if (callback != null) tweenSequence.OnComplete(() => callback());
       
        return tweenSequence;
    }
    
    #region Canvas Fade
    [Serializable]
    public class CanvasFadeTween
    {
        public Ease Ease;
        public float Time;
        public float Delay;
    }
    
    public static Tween SetCanvasFade(Ease ease, CanvasGroup canvas,bool visible, float time,float delay)
    {
       Tweener tween = canvas.DOFade(visible ? 1 : 0, time);
       tween.SetEase(ease);
        tween.SetDelay(delay);
       return tween;
    }
    #endregion
    
    #region RectPosition
    
    [Serializable]
    public class RectTransformPosTween
    {
        public Ease Ease;
        public Vector2 Position;
        public float Time;
        public float Delay;
        public bool Snap;
    }
    
    public static Tween SetRectTPosition(RectTransform rect,Ease ease,Vector2 pos,float time,float delay,bool snap)
    {
        Tweener tween = rect.DOAnchorPos(pos, time, snap);
        tween.SetEase(ease);
        tween.SetDelay(delay);
        return tween;
    }

    #endregion
    [Serializable]
    public class RectTransformScaleTween
    {
        public Ease Ease;
        public Vector3 Scale;
        public float Time;
        public float Delay;
    }

    public static Tween SetRectTScale(RectTransform rect,Ease ease, Vector3 scale,float time,float delay)
    {
        Tweener tween = rect.DOScale(scale, time);
        tween.SetEase(ease);
        tween.SetDelay(delay);
        return tween;
    }


}
#endif