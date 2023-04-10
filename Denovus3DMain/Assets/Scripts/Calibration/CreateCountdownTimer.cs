using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCountdownTimer : MonoBehaviour
{
    public GameObject timerObject;
    // Start is called before the first frame update
    public void CreateTimer()
    {
        GameObject timer = Instantiate(timerObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        timer.transform.SetParent(transform, false);
    }
}
