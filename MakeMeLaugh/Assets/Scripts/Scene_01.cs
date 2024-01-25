using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] GameObject[] rainSprite;

    public enum State
    {
        Sleep, Walk
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        // Rain animation in background
        Common.Anim(rainSprite);
    }
}
