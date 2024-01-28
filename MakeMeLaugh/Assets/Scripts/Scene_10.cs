using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_10 : MonoBehaviour
{
    [SerializeField] Master master;

    [Space(10)]
    [Header("Scene")]

    [SerializeField] GameObject[] vader;
    [SerializeField] GameObject[] vaderSnap;
    [SerializeField] GameObject[] wearSprite;
    [SerializeField] GameObject button;

    float startTime;
    bool isGrabbing = false;
    GameObject grabbingObject;
    float snapDist = 2.0f;
    //bool wearHead = false;
    //bool wearSaber = false;
    //bool wearLeft = false;
    //bool wearRight = false;

    public void Init()
    {
        startTime = Time.time;
    }

    void Start()
    {
        Method.ArraySetActive(wearSprite, -1);
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
                CheckSnapPoints(grabbingObject);
            }
        }

        if (Time.time > startTime + 5.0f)
        {
            button.SetActive(true);
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

    bool CheckSnapPoints(GameObject grabbing)
    {
        switch (grabbing.name)
        {
            case "vader-head":
                if (Dist("head_vader_snap") < snapDist) Snap("head_vader_snap");
                if (Dist("head_body_snap") < snapDist)
                {
                    Snap("head_body_snap");
                    master.wearHead = true;
                }
                break;
            case "vader-hand":
                if (Dist("hand_vader_snap") < snapDist) Snap("hand_vader_snap");
                break;
            case "vader-saber":
                if (Dist("saber_vader_snap") < snapDist) Snap("saber_vader_snap");
                if (Dist("saber_body_snap") < snapDist)
                {
                    Snap("saber_body_snap");
                    master.wearSaber = true;
                }
                break;
            case "vader-body":
                if (Dist("body_vader_snap") < snapDist) Snap("body_vader_snap");
                break;
            case "vader-left-foot":
                if (Dist("left_vader_snap") < snapDist) Snap("left_vader_snap");
                if (Dist("left_body_snap") < snapDist) Snap("left_body_snap");
                break;
            case "vader-right-foot":
                if (Dist("right_vader_snap") < snapDist) Snap("right_vader_snap");
                if (Dist("right_body_snap") < snapDist) Snap("right_body_snap");
                break;
            case "vader-base":
                if (Dist("base_vader_snap") < snapDist) Snap("base_vader_snap");
                break;
        }
        return false;

        float Dist(string check)
        {
            return Vector3.Distance(grabbing.transform.position, GameObject.Find(check).transform.position);
        }

        void Snap(string check)
        {
            grabbing.transform.position = GameObject.Find(check).transform.position;
            grabbing.transform.rotation = GameObject.Find(check).transform.rotation;
        }

        /*void Wear(string part)
        {
            if (part == "head_body_snap")
            {
                if (wearSaber)
                {
                    Method.ArraySetActive(wearSprite, 2);
                }
                else
                {
                    Method.ArraySetActive(wearSprite, 0);
                }
            }
            if (part == "saber_body_snap")
            {
                if (wearHead)
                {
                    Method.ArraySetActive(wearSprite, 2);
                }
                else
                {
                    Method.ArraySetActive(wearSprite, 1);
                }
            }
        }*/
    }
}
