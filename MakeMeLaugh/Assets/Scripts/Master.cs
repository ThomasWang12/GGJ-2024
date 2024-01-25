using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject[] sceneCanvas;

    public int activeScene = 0;

    void Start()
    {
        Common.Init();

        activeScene = 1;
        SetScene(activeScene);
    }

    void Update()
    {

    }

    void SetScene(int index)
    {
        for (int i = 0; i < sceneCanvas.Length; i++)
            sceneCanvas[i].SetActive(i == index);
    }
}
