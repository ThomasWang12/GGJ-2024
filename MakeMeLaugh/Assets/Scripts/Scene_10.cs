using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_10 : MonoBehaviour
{
    [SerializeField] Master master;

    [Space(10)]
    [Header("Scene")]

    [SerializeField] GameObject[] vader;

    bool isGrabbing = false;
    GameObject grabbingObject;

    void Start()
    {

    }

    void Update()
    {
        if (!isGrabbing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MouseOnAnyCollider())
                {
                    isGrabbing = true;
                    GetMouseCollidedObject();
                }
            }
        }
        else
        {
            grabbingObject.transform.position = master.mouse;
            grabbingObject.transform.rotation = Quaternion.identity;

            if (Input.GetMouseButtonUp(0))
            {
                isGrabbing = false;
                CheckSnapPoints(gameObject.name);
            }
        }

    }

    bool MouseOnAnyCollider()
    {
        for (int i = 0; i < vader.Length; i++)
        {
            if (Method.MouseOnCollider(vader[i].transform.Find("grab").gameObject))
                return true;
        }
        return false;
    }

    void GetMouseCollidedObject()
    {
        for (int i = 0; i < vader.Length; i++)
        {
            if (Method.MouseOnCollider(vader[i].transform.Find("grab").gameObject))
                grabbingObject = vader[i];
        }
    }

    bool CheckSnapPoints(string name)
    {
        switch (name)
        {
            case "vader-head":
                break;
            case "vader-hand":
                break;
            case "vader-saber":
                break;
            case "vader-body":
                break;
            case "vader-left-foot":
                break;
            case "vader-right-foot":
                break;
            case "vader-base":
                break;
        }
        return false;
    }
}
