using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject[] sceneCanvas;

    public int activeScene = 0;
    [HideInInspector] public Vector2 mouse;

    public PlayerState playerState;
    public enum PlayerState
    {
        MoveInput, InAction
    }

    void Awake()
    {
        Common.Init();
    }

    void Start()
    {
        GoToScene(1);
    }

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GoToScene(int index)
    {
        activeScene = index;
        Method.ArraySetActive(sceneCanvas, activeScene);
    } 
}
