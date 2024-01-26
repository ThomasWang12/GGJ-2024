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
    bool isSleeping = false;
    int sleepStep = 0;

    void Start()
    {

    }

    void Update()
    {
        pos = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    #region Scene 01

    public void Scene01_Init()
    {
        MoveEnd();
    }

    public void Scene01_Input()
    {
        if (!master.allowInput) return;

        walking = true;
        target = mouse;
        float duration = Mathf.Abs(pos.x - target.x) / walkSpeed;
        transform.DOKill();
        transform.DOMoveX(target.x, duration).SetEase(Ease.Linear);
        walkDir = (pos.x > target.x) ? direction.Left : direction.Right;
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
        //if (isSleeping)
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