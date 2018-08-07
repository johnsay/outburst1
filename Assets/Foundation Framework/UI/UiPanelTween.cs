#if DOTWEEN
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class UiPanelTween : UiPanelBase
{
    private Sequence _panelSequence;
    private Tween[] _showTweens;
    private Tween[] _hideTweens;

    [SerializeField] private bool _enableCanvasFade;
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private UiTween.CanvasFadeTween _canvasFadeOnShow;
    [SerializeField] private UiTween.CanvasFadeTween _canvasFadeOnHide;

    [SerializeField] private bool _enableRectPos;
    [SerializeField] private UiTween.RectTransformPosTween _rectPosOnShow;
    [SerializeField] private UiTween.RectTransformPosTween _rectPosOnHide;
    
    [SerializeField] private bool _enableRectScale;
    [SerializeField] private UiTween.RectTransformScaleTween _rectScaleOnShow;
    [SerializeField] private UiTween.RectTransformScaleTween _rectScaleOnHide;

    public override void Awake()
    {
        OnLevelLoaded();
        if (ForceResetPosition)
        {
            RectTransform.anchoredPosition = _enableRectPos? _rectPosOnHide.Position:Vector2.zero;
        }

        if (ForceHide)
        {
            if (_enableRectScale) RectTransform.localScale = _rectScaleOnHide.Scale;
        }
    }

    protected override void OnLevelLoaded()
    {
        base.OnLevelLoaded();
        _panelSequence.SafeKill();
    }

    private void OnDestroy()
    {
        _panelSequence.SafeKill();
    }

    public override void HideFirst()
    {
        base.HideFirst();
        if (_enableCanvasFade) _canvas.alpha = 0;
        
    }

    private void GetShowTweens()
    {
        List<Tween> showList = new List<Tween>();
        
        
        if (_enableCanvasFade)
        {
            showList.Add(UiTween.SetCanvasFade(_canvasFadeOnShow.Ease,_canvas,true,_canvasFadeOnShow.Time,_canvasFadeOnShow.Delay));
        }
        if (_enableRectPos)
        {
            showList.Add(UiTween.SetRectTPosition(RectTransform,_rectPosOnShow.Ease,_rectPosOnShow.Position,_rectPosOnShow.Time,_rectPosOnShow.Delay,_rectPosOnShow.Snap));
         }
        if (_enableRectScale)
        {
            showList.Add(UiTween.SetRectTScale(RectTransform,_rectScaleOnShow.Ease,_rectScaleOnShow.Scale,_rectScaleOnShow.Time,_rectScaleOnShow.Delay));
        }
        
        _showTweens = showList.ToArray();
    }

    private void GetHideTweens()
    {
        List<Tween> hideList = new List<Tween>();
        
        if (_enableCanvasFade)
        {
           hideList.Add(UiTween.SetCanvasFade(_canvasFadeOnHide.Ease,_canvas,false,_canvasFadeOnHide.Time,_canvasFadeOnHide.Delay));
        }
        if (_enableRectPos)
        {
           hideList.Add(UiTween.SetRectTPosition(RectTransform,_rectPosOnHide.Ease,_rectPosOnHide.Position,_rectPosOnHide.Time,_rectPosOnHide.Delay,_rectPosOnHide.Snap));
        }
        if (_enableRectScale)
        {
            hideList.Add(UiTween.SetRectTScale(RectTransform,_rectScaleOnHide.Ease,_rectScaleOnHide.Scale,_rectScaleOnHide.Time,_rectScaleOnHide.Delay));
        }
        _hideTweens = hideList.ToArray();
    }

    public override void Show(bool triggerEvents)
    {
        if (IsVisible)
            return;

        IsVisible = true;
        SetChildrenActive(true);

        _panelSequence.SafeKill();
        GetShowTweens();
        
        _panelSequence = UiTween.BuildUiSequence(_showTweens);
        
        
        if (triggerEvents)
            InvokeShowEvent();
    }

    public override void Hide(bool triggerEvents)
    {
        if (IsVisible == false)
            return;

        IsVisible = false;
        _panelSequence.SafeKill();
        GetHideTweens();
        _panelSequence = UiTween.BuildUiSequence(_hideTweens, () =>
        {
            SetChildrenActive(false); 
        });
     

        if (triggerEvents)
            InvokeHideEvent();
    }
}
#endif