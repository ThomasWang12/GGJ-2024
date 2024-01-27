using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01 : MonoBehaviour
{
    [SerializeField] Master master;
    public enum State { Sleep, Walk }
    public State state;

    [SerializeField] GameObject[] backgroundSprite;
    [SerializeField] GameObject[] rainSprite;
    [SerializeField] GameObject[] sleepSprite;
    [SerializeField] GameObject bed;

    [SerializeField] GameObject player;
    [SerializeField] GameObject idleSprite;
    [SerializeField] GameObject[] walkLeftSprite;
    [SerializeField] GameObject[] walkRightSprite;

    Vector2 pos;

    bool isWalking = false;
    enum dir { Left, Right };
    dir walkDir;
    Vector2 walkTarget;
    float walkStartTime;
    float walkSpeed = 2f;
    float walkAnimFrameTime = 0.333f;

    bool isGettingInBed = false;
    float besideBedX = -3.85f;
    int sleepStep = 0;
    int sleepStepMin = 0;
    int sleepStepMax = 100;

    void Start()
    {
        Method.ArraySetActive(sleepSprite, -1);
        state = State.Walk;
        WalkEnd();
    }

    void Update()
    {
        // Rain animation in background
        Method.Anim(rainSprite);

        // Player position
        pos = player.transform.position;

        if (state == State.Walk)
        {
            Method.ArraySetActive(backgroundSprite, 0);

            if (Method.Click())
            {
                // If player isn't clicking the bed
                if (!Method.MouseOnCollider(bed))
                {
                    // Walk to that point
                    WalkTo(master.mouse);
                }
                // Clicks the bed, go to sleep
                else
                {
                    GoToBed();
                    Debug.Log("Going to bed");
                }
            }

            // Character sprite
            idleSprite.SetActive(!isWalking);
            if (isWalking)
            {
                // Walking animation
                if (walkDir == dir.Left)
                {
                    Method.Anim_FrameTime(walkStartTime, walkLeftSprite, walkAnimFrameTime);
                    Method.ArraySetActive(walkRightSprite, -1);
                }
                if (walkDir == dir.Right)
                {
                    Method.Anim_FrameTime(walkStartTime, walkRightSprite, walkAnimFrameTime);
                    Method.ArraySetActive(walkLeftSprite, -1);
                }
                // Walking: reach the target
                if (Mathf.Abs(pos.x - walkTarget.x) < Common.margin)
                    WalkEnd();
                // Walking: reach the bed
                if (pos.x <= besideBedX + Common.margin)
                {
                    WalkEnd();
                    idleSprite.SetActive(false);
                    state = State.Sleep;
                    isGettingInBed = true;
                }
            }
        }

        if (state == State.Sleep)
        {
            Method.ArraySetActive(backgroundSprite, 1);

            Debug.Log(sleepStep);

            if (Method.Click())
            {
                // If player isn't clicking the bed
                if (!Method.MouseOnCollider(bed))
                {
                    // Get up from bed
                    sleepStep = 50;
                    isGettingInBed = false;
                    walkTarget = master.mouse;
                }
            }

            // Sleep step 1...49: wake-up sprite
            if (sleepStep > 0 && sleepStep < 50)
                Method.ArraySetActive(sleepSprite, 0);

            // Sleep step 50...100: sleeping sprite
            if (sleepStep >= 50 && sleepStep <= 100)
                Method.ArraySetActive(sleepSprite, 1);

            // Get up and walk
            if (!isGettingInBed && sleepStep == 0)
            {
                Method.ArraySetActive(sleepSprite, -1);
                state = State.Walk;
                WalkTo(walkTarget);
            }
        }
    }

    void FixedUpdate()
    {
        if (state == State.Sleep)
        {
            if (isGettingInBed)
            {
                if (sleepStep < sleepStepMax) sleepStep++;
            }
            else
            {
                if (sleepStep > sleepStepMin) sleepStep--;
            }
        }
    }

    void WalkTo(Vector2 click)
    {
        isWalking = true;
        if (click.x < pos.x) walkDir = dir.Left;
        if (click.x > pos.x) walkDir = dir.Right;
        player.transform.DOKill();
        float duration = Mathf.Abs(pos.x - click.x) / walkSpeed;
        player.transform.DOMoveX(click.x, duration).SetEase(Ease.Linear);
        walkTarget = click;
        walkStartTime = Time.time;
    }

    void WalkEnd()
    {
        isWalking = false;
        walkTarget = Vector2.zero;
        player.transform.DOKill();
        Method.ArraySetActive(walkLeftSprite, -1);
        Method.ArraySetActive(walkRightSprite, -1);
    }

    void GoToBed()
    {
        if (pos.x > besideBedX)
        {
            WalkTo(new Vector2(besideBedX, 0));
        }
    }
}
