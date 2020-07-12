using System;
using UnityEngine;

public static class Utils
{
	public static void Scale(Transform transform, float scrollDelta)
    {
        transform.localScale = scrollDelta > 0 ? transform.localScale / Constants.SCALE_TICK : transform.localScale * Constants.SCALE_TICK;
    }

    public static float Scale(float baseValue, float scrollDelta)
    {
       return scrollDelta > 0 ? baseValue / Constants.SCALE_TICK : baseValue * Constants.SCALE_TICK;
    }

    public static Vector3 Scale(Vector3 baseValue, float scrollDelta)
    {
        return scrollDelta > 0 ? baseValue / Constants.SCALE_TICK : baseValue * Constants.SCALE_TICK;
    }
}
