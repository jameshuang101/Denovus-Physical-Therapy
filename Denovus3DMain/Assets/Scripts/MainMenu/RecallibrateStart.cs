using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallibrateStart : MonoBehaviour
{
    public void Recallibrate()
    {
        GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().SwitchMainToCallibration();
    }
}
