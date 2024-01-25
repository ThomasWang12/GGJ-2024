using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Common
{
    public static float margin = 0.01f;

    static float fps = 6f;
    static float frameTime = 0;

    public static void Init()
    {
        frameTime = 1 / fps;
    }

    public static void ArraySetActive(GameObject[] array, int index)
    {
        for (int i = 0; i < array.Length; i++)
            array[i].SetActive(i == index);
    }

    public static void Anim(GameObject[] sprites)
    {
        int currentFrame = Mathf.FloorToInt((Time.time % (frameTime * sprites.Length)) / frameTime);
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].SetActive(i == currentFrame);
    }
}
