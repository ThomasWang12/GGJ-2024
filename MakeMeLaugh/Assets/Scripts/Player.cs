using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] GameObject idleSprite;
    [SerializeField] GameObject[] walkLeftSprite;
    [SerializeField] GameObject[] walkRightSprite;

    Vector2 pos;
    Vector2 mouse;
    Vector2 target;

    bool walking = false;
    enum direction { Left, Right };
    direction walkDir;
    float walkSpeed = 3f;

    bool init_01 = false;

    void Start()
    {

    }

    void Update()
    {
        pos = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (master.isPlaying)
        {
            switch (master.activeScene)
            {
                case 0: break;
                case 1: Scene_01(); break;
            }
        }
    }

    void Scene_01()
    {
        if (!init_01)
        {
            MoveEnd();
            init_01 = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            walking = true;
            target = mouse;
            float duration = Mathf.Abs(pos.x - target.x) / walkSpeed;
            transform.DOKill();
            transform.DOMoveX(target.x, duration).SetEase(Ease.Linear);
            walkDir = (pos.x > target.x) ? direction.Left : direction.Right;
        }

        idleSprite.SetActive(!walking);
        if (walking)
        {
            if (walkDir == direction.Left)
            {
                Common.Anim(walkLeftSprite);
                Common.ArraySetActive(walkRightSprite, -1);
            }
            if (walkDir == direction.Right)
            {
                Common.Anim(walkRightSprite);
                Common.ArraySetActive(walkLeftSprite, -1);
            }
            if (Mathf.Abs(pos.x - target.x) < Common.margin)
                MoveEnd();
        }
    }

    void MoveEnd()
    {
        walking = false;
        target = Vector2.zero;
        transform.DOKill();
        idleSprite.SetActive(true);
        Common.ArraySetActive(walkLeftSprite, -1);
        Common.ArraySetActive(walkRightSprite, -1);
    }
}