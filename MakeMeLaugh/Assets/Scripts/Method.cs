using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Method
{
    public static bool Click()
    {
        return Input.GetMouseButtonDown(0);
    }

    public static bool MouseOnCollider(GameObject gameObject)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return gameObject.GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    public static bool PlayerOnCollider(Vector3 playerPosition, GameObject gameObject)
    {
        return gameObject.GetComponent<Collider2D>().OverlapPoint(playerPosition);
    }

    public static void ArraySetActive(GameObject[] array, int index)
    {
        for (int i = 0; i < array.Length; i++)
            array[i].SetActive(i == index);
    }

    public static void Anim(GameObject[] sprites)
    {
        int currentFrame = Mathf.FloorToInt((Time.time % (Common.frameTime * sprites.Length)) / Common.frameTime);
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].SetActive(i == currentFrame);
    }

    public static void Anim_FrameTime(float startTime, GameObject[] sprites, float frameTime)
    {
        int currentFrame = Mathf.FloorToInt(((Time.time - startTime) % (frameTime * sprites.Length)) / frameTime);
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].SetActive(i == currentFrame);
    }

    public static float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
