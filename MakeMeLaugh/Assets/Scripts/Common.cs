using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    static float fps = 6f;
    static float frameTime = 0;

    public static void Init()
    {
        frameTime = 1 / fps;
    }

    public static void Anim(GameObject[] anim, int frameCount)
    {
        int currentFrame = Mathf.FloorToInt((Time.time % (frameTime * frameCount)) / frameTime);
        for (int i = 0; i < anim.Length; i++)
            anim[i].SetActive(i == currentFrame);
    }
}
