using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] Player_01 player;
    public GameObject bed;
    [SerializeField] GameObject[] rainSprite;
    public GameObject[] sleepSprite;

    public enum State { Init, Sleep, Walk }
    public State state;

    void FixedUpdate()
    {
        // Rain animation in background
        Common.Anim(rainSprite);

        switch (state)
        {
            case State.Init:
                player.Scene01_Init();
                break;
            case State.Sleep:
                player.Scene01_Input();
                player.Scene01_Sleep();
                break;
            case State.Walk:
                player.Scene01_Input();
                player.Scene01_Walk();
                break;
        }
    }
}
