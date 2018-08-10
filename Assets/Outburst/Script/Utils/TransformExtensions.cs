
using UnityEngine;

public static class TransformExtensions
{
	public static Vector3 ChangeLocalScaleX(this Transform transform, float x)
	{
		Vector3 scale = transform.localScale;
		scale.x = x;
		transform.localScale = scale;
		return scale;
	}

	public static Vector3 ChangeLocalScaleY(this Transform transform, float y)
	{
		Vector3 scale = transform.localScale;
		scale.y = y;
		transform.position = scale;
		return scale;
	}
	
}
