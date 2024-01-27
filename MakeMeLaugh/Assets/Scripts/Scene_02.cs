using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_02 : MonoBehaviour
{
    [SerializeField] Master master;
    public enum State { Walk, End }
    public State state;

    [Space(10)]
    [Header("Scene")]

    [SerializeField] GameObject door;

    [Space(10)]
    [Header("Player")]

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] walkSprite;

    Vector2 pos;

    bool isWalking = false;
    Vector2 walkTarget;
    float walkStartTime;
    float walkSpeed = 2f;
    float walkProgress;
    float walkProgMin = 1.6f;
    float walkProgMax = 16;

    void Start()
    {
        Method.ArraySetActive(walkSprite, 0);
        state = State.Walk;
    }

    void Update()
    {
        // Player position
        pos = player.transform.position;

        if (state == State.Walk)
        {
            // Update player position and scale based on walkStep
            walkProgress = Mathf.Abs(pos.x - door.transform.position.x);
            float y = Method.Map(walkProgress, walkProgMin, walkProgMax, -0.2f, -7.5f);
            float scale = Method.Map(walkProgress, walkProgMin, walkProgMax, 66.6666f, 120);
            player.transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
            player.transform.localScale = new Vector3(scale, scale, scale);

            if (Method.Click())
            {
                // Walk to that point
                WalkTo(master.mouse);
            }

            if (Method.PlayerOnCollider(pos, door))
            {
                // Next scene
                WalkEnd();
                state = State.End;
                master.EndScene(2);
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
