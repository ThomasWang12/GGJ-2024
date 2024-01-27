using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_07 : MonoBehaviour
{
    [SerializeField] Master master;

    float waitTime = 1.75f;

    public void AutoSwitch()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        master.EndScene(7);
    }
}
