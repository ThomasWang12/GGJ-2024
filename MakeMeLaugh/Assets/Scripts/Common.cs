using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static float margin = 0.01f;
    public static float fps = 6f;
    public static float frameTime = 0;
    public static float walkAnimFrameTime = 0.333f;

    public static void Init()
    {
        frameTime = 1 / fps;
    }
}
