using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject[] sceneCanvas;

    [HideInInspector] public bool isPlaying = false;
    [HideInInspector] public int activeScene = 0;

    void Start()
    {
        Common.Init();

        isPlaying = true;
        activeScene = 1;
        Common.ArraySetActive(sceneCanvas, activeScene);
    }

    void Update()
    {

    }
}
