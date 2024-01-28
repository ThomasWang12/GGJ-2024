using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_03 : MonoBehaviour
{
    [SerializeField] Master master;
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

        Method.ArraySetActive(slides, num);

        if (num == 5)
        {
            master.EndScene(3);
        }
    }
}
