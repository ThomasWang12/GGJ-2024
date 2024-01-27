using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_08 : MonoBehaviour
{
    [SerializeField] Master master;
    public enum State { Walk, End }
    public State state;

    [Space(10)]
    [Header("Scene")]

    [SerializeField] GameObject[] backgroundSprite;
    [SerializeField] GameObject vader;

    [Space(10)]
    [Header("Player")]

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] walkSprite;

    Vector2 pos;

    bool isWalking = false;
    Vector2 walkTarget;
    float walkStartTime;
    float walkSpeed = 2f;

    float endX = -4.6f;

    void Start()
    {

    }

    void Update()
    {
        // Player position
        pos = player.transform.position;

        if (Method.Click())
        {
            // Walk to that point
            WalkTo(master.mouse);
        }

        // Character sprite
        walkSprite[0].SetActive(!isWalking);
        if (isWalking)
        {
            // Walking animation
            Method.Anim_FrameTime(walkStartTime, walkSprite, Common.walkAnimFrameTime);

            // Walking: reach the target
            if (Mathf.Abs(pos.x - walkTarget.x) < Common.margin)
                WalkEnd();

            // Walking: reach the fall point
            if (pos.x >= endX)
            {
                // Next scene
                WalkEnd();
                walkSprite[0].SetActive(true);
                state = State.End;
                master.EndScene(8);
            }
        }
    }

    void WalkTo(Vector2 click)
    {
        isWalking = true;
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
        Method.ArraySetActive(walkSprite, 0);
    }
}
