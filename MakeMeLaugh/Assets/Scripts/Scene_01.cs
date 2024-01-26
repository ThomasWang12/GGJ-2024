using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] Player player;
    [SerializeField] GameObject[] rainSprite;

    public enum State
    {
        Init, Sleep, Walk
    }
    public State State_01;

    void FixedUpdate()
    {
        // Rain animation in background
        Common.Anim(rainSprite);

        switch (State_01)
        {
            case State.Init:
                player.Scene01_Init();
                break;
            case State.Sleep:
                if (Input.GetMouseButtonDown(0))
                    player.Scene01_Input();
                player.Scene01_Sleep();
                break;
            case State.Walk:
                if (Input.GetMouseButtonDown(0))
                    player.Scene01_Input();
                player.Scene01_Walk();
                break;
        }
    }
}
