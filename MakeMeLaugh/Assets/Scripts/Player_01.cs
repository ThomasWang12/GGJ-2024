using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_01 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] Scene_01 scene;
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
    int sleepStep = 0;

    void Start()
    {

    }

    void Update()
    {
        pos = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mathf.Clamp(sleepStep, 0, 100);
    }

    #region Scene 01

    public void Scene01_Init()
    {
        MoveEnd();
    }

    public void Scene01_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (sleepStep == 0)
            {
                // If not clicking the bed
                if (!scene.bed.GetComponent<Collider2D>().OverlapPoint(Input.mousePosition))
                {
                    walking = true;
                    target = mouse;
                    float duration = Mathf.Abs(pos.x - target.x) / walkSpeed;
                    transform.DOKill();
                    transform.DOMoveX(target.x, duration).SetEase(Ease.Linear);
                    walkDir = (pos.x > target.x) ? direction.Left : direction.Right;
                }
            }
            else
            {
                // If not clicking the bed
                if (!scene.bed.GetComponent<Collider2D>().OverlapPoint(Input.mousePosition))
                {
                    sleepStep--;
                }
            }
        }
    }

    public void Scene01_Walk()
    {
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
        else
        {
            idleSprite.SetActive(true);
        }
    }

    public void Scene01_Sleep()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            // Sleeping
            if (scene.state == Scene_01.State.Sleep)
            {
                // Get out of bed
                if (!scene.bed.GetComponent<Collider2D>().OverlapPoint(Input.mousePosition))
                {
                    scene.state = Scene_01.State.Walk;
                }
            }
        }
        else
        {
            // Go to sleep
            if (scene.bed.GetComponent<Collider2D>().OverlapPoint(pos))
            {
                scene.state = Scene_01.State.Sleep;
            }
        }*/

        // sleepStep 0...49
        if (sleepStep >= 0 && sleepStep <= 49)
        {
            Common.ArraySetActive(scene.sleepSprite, 0);
        }
        // sleepStep 50...100
        if (sleepStep >= 50 && sleepStep <= 100)
        {
            Common.ArraySetActive(scene.sleepSprite, 1);
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

    #endregion
}