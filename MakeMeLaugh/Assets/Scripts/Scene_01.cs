using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] GameObject[] rainAnim;

    void Start()
    {
    }

    void FixedUpdate()
    {
        // Rain animation in background
        Common.Anim(rainAnim, rainAnim.Length);
    }
}
