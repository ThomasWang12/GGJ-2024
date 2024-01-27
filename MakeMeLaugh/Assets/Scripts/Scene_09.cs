using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_09 : MonoBehaviour
{
    [SerializeField] Master master;
    public enum State { Wait, Fall, End }
    public State state;

    [Space(10)]
    [Header("Scene")]

    [SerializeField] GameObject[] vader;
    [SerializeField] GameObject[] vaderFall;

    [Space(10)]
    [Header("Player")]

    [SerializeField] GameObject[] playerSprite;

    float firstWaitTime = 0.25f;
    float fallDuration = 4.5f;

    void Update()
    {

    }

    public void Init()
    {
        state = State.Wait;
        StartCoroutine(FirstWait());
    }

    IEnumerator FirstWait()
    {
        yield return new WaitForSeconds(firstWaitTime);
        Falling();
    }

    void Falling()
    {
        state = State.Fall;
        playerSprite[0].GetComponent<SpriteRenderer>().DOFade(0, 1.5f).SetEase(Ease.Linear);
        playerSprite[1].GetComponent<SpriteRenderer>().DOFade(1, 1.5f).SetEase(Ease.Linear);
        for (int i = 0; i < vader.Length; i++)
        {
            Vector3 fallPos = vaderFall[i].transform.position;
            Vector3 fallRot = vaderFall[i].transform.eulerAngles;
            vader[i].transform.DOMove(fallPos, fallDuration).SetEase(Ease.Linear);
            vader[i].transform.DORotate(fallRot, fallDuration).SetEase(Ease.Linear);
        };
        StartCoroutine(Fall_End());
    }

    IEnumerator Fall_End()
    {
        yield return new WaitForSeconds(fallDuration - 0.5f);
        master.EndScene(9);
    }
}
