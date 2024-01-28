using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_11 : MonoBehaviour
{
    [SerializeField] Master master;
    [SerializeField] GameObject[] normalSlides;
    [SerializeField] GameObject[] wearSlides;
    [SerializeField] GameObject[] slides;

    int num = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            num++;
        }

        if (num < 2)
        {
            if (master.wearHead)
            {
                Method.ArraySetActive(wearSlides, num);
            }
            else Method.ArraySetActive(normalSlides, num);
        }
        else
        {
            Method.ArraySetActive(slides, num - 3);
        }
    }
}
