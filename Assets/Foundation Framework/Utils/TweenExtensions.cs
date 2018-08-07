#if DOTWEEN
using DG.Tweening;

public static class TweenExtensions 
{
	public static void SafeKill(this Tween tween)
	{
		if (tween != null && tween.IsPlaying())
		{
			tween.Kill();
		}
	}
	
	public static void SafeKill(this Sequence sequence)
	{
		if (sequence != null && sequence.IsPlaying())
		{
			sequence.Kill();
		}
	}
	
}
#endif
